using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Fileholder.Domain.Abstract;
using Fileholder.Domain;
using Fileholder.Models;
using Microsoft.AspNet.Identity;
using log4net;

namespace Fileholder.Controllers
{
    public class UploadFilesController : Controller
    {
        #region Repository
        private IAllFilesRepository RepositoryAllFiles;
        private IGroupFilesOfOneUploadRepository RepositoryGroupFiles;
        private IAllFilesAndGroupFilesLinkRepository RepositoryLinkFiles;
        private IFileConfigsRepository RepositoryFileConfigs;
        private static readonly ILog log = Logging.LogManager.ViewFileLog();

        public UploadFilesController(
            IAllFilesRepository allFiles,
            IGroupFilesOfOneUploadRepository groupFiles,
            IAllFilesAndGroupFilesLinkRepository linkFiles,
            IFileConfigsRepository fileConfigs
            )
        {
            RepositoryAllFiles = allFiles;
            RepositoryGroupFiles = groupFiles;
            RepositoryLinkFiles = linkFiles;
            RepositoryFileConfigs = fileConfigs;
        }
        #endregion

        
        #region ShowUploadedFile
        /// <summary>
        /// Выводит загруженные файлы, которые были загружены за один раз, все вместе. Если файлов
        /// с таким fileGuid нет в базе, то возвращается главная страница
        /// </summary>
        /// <param name="fileGuid">fileGuid файлов</param>
        /// <returns>Возвращается страница с файлами</returns>
        [HttpGet]
        public ActionResult ShowUploadedFile(string id)
        {           
            string fileGuid = id;
            List<AllFiles> AllFiles = RepositoryAllFiles.GetFilesByFileGuid(fileGuid);

            //Если файлы нашлись, проверяется не вышел ли срок хранения.
            //Если срок хранения вышел, то файлы удаляются из папок и все записи, 
            //которые относятся к этим файлам, так же удаляются из БД
            if (AllFiles != null)
            {
                if ((RepositoryFileConfigs.CheckDeleteDate(fileGuid)))
                {
                    DeleteGroupFiles(fileGuid);
                    return RedirectToAction("Index", "Home");
                }
            }

            //если файлы не нашлись, то перенаправляем на главный экран, без вывода ошибок
            if (AllFiles == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (AllFiles.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            //Формируем представление требуемых файлов и выводим на экран
            int totalFilesSize = 0;
            List<ShowUploadedFilesViewModel> showUploadedFilesViewModel = new List<ShowUploadedFilesViewModel>();
            FileConfigs fileConfigs = RepositoryFileConfigs.GetConfigsByFileGuid(fileGuid);

            foreach (var item in AllFiles)
            {
                totalFilesSize += item.FileSize;
            }

            foreach (var item in AllFiles)
            {
                showUploadedFilesViewModel.Add(new ShowUploadedFilesViewModel
                {
                    FileExtension = item.FileExtension,
                    FileGuid = fileGuid,
                    FileId = item.FileId,
                    FileName = item.FileName,
                    FileSize = GetFileSize(item.FileSize),
                    FileUploadTime = RepositoryGroupFiles.GetUploadDate(fileGuid),
                    TotalSizeAllFiles = GetFileSize(totalFilesSize),
                    UserLogin = RepositoryGroupFiles.GetUserName(fileGuid),
                    FilePassword = null,
                    NumberOfDownload = (int)fileConfigs.DownloadCounter,
                    FileDeleteTime = fileConfigs.DateOfDeletion
                });
            }

            ViewBag.LinkForDownload = Request.Url.Authority + "/" + fileGuid;
            ViewBag.CurrentUser = showUploadedFilesViewModel[0].UserLogin;
            log.Info(" - VIEW FILE\nFile id: " + fileGuid + "\nUser: " + ViewBag.CurrentUser + "\nUser IP: " + GetUserIP());
            return View(showUploadedFilesViewModel);
        }
        #endregion

        #region SaveFiles
        //TODO: добавить визуализацию(индикатор) загрузки файлов
        /// <summary>
        /// Принимает массив файлов не более 100мб, записывает инфу о файлах в базу и сохраняет все файлы в папку AllUserFiles.
        /// Для каждого набора файла генерируется fileGuid, создается папка с таким же названием(fileGuid).
        /// Каждый отдельный файл сохраняется в отдельную папку, которая создается и называется айдишником этого файла.
        /// </summary>
        /// <param name="file">Набор файлов</param>
        /// <returns>Возвращается страница, которая выводит все загруженные файлы</returns>
        [HttpPost]
        public ActionResult SaveFiles(HttpPostedFileBase[] file)
        {
            if (file == null || file[0] == null)
            {
                return RedirectToAction("ErrorPage", "ErrorController");
            }

            const int NUMBER_OF_ATTEMPTS_TO_GENERATE = 10;
            const string ROOT_PATH = @"~\AllUsersFiles\";
            string fileGuid = null;

            #region генерация guid для папки файла
            //если за 10 попыток не генерируется уникальный guid, то возвращается страница с ошибкой
            for (int i = 0; i < NUMBER_OF_ATTEMPTS_TO_GENERATE; i++)
            {
                fileGuid = CreateFolderGuid();

                if (fileGuid != null)
                    break;
            }

            if (fileGuid == null)
            {
                return RedirectToAction("ErrorPage", "ErrorController");
            }
            #endregion

            string fullPathForAllFiles = ROOT_PATH + fileGuid;
            int userFilesIndexCounter = 0;
            string fullPathForEachFiles;
            Directory.CreateDirectory(Server.MapPath(fullPathForAllFiles));

            List<AllFiles> allFiles = new List<AllFiles>(file.Length);

            AllFilesAndGroupFilesLink filesLink = new AllFilesAndGroupFilesLink();

            GroupFileOfOneUpload groupFilesOfOneUpload = new GroupFileOfOneUpload
            {
                UserName = Request.IsAuthenticated ? User.Identity.GetUserName() : "Anonymous",
                FileUploadDate = DateTime.Now,
                GroupFileGuid = fileGuid
            };

            //Добавляем в таблицу групповую инфу о файлах
            RepositoryGroupFiles.AddGroup(groupFilesOfOneUpload);

            //Добавляем дефолтную инфу о файле
            new FileConfigsController(groupFilesOfOneUpload, RepositoryFileConfigs);

            //получаем из файлов инфу и добавляем ее в таблицу по каждому файлу
            foreach (var item in file)
            {
                allFiles.Add(new AllFiles
                {
                    FileName = item.FileName,
                    FileExtension = Path.GetExtension(item.FileName),
                    //FileGuid = fileGuid,
                    FileSize = item.ContentLength,
                    //FileUploadTime = DateTime.Now,
                    //UserLogin = Request.IsAuthenticated ? User.Identity.GetUserName() : "Anonymous"
                });

                RepositoryAllFiles.AddFile(allFiles[userFilesIndexCounter]);

                //Добавляем в линкующую таблицу, айдишники отдельных файлов и айдишник группы этих файлов
                filesLink.FileId = allFiles[userFilesIndexCounter].FileId;
                filesLink.GroupFileId = groupFilesOfOneUpload.GroupFileId;
                RepositoryLinkFiles.AddLink(filesLink);

                fullPathForEachFiles = Server.MapPath(fullPathForAllFiles) + @"\" + allFiles[userFilesIndexCounter].FileId;

                Directory.CreateDirectory(fullPathForEachFiles);
                item.SaveAs(fullPathForEachFiles + @"\" + item.FileName);
                userFilesIndexCounter++;
            }

            return RedirectToAction("ShowUploadedFile", new { id = fileGuid });
        }
        #endregion

        [HttpGet]
        public ActionResult ShowAllUserFiles()
        {
            List<string> fileGuids = RepositoryGroupFiles.GetAllUserFile(User.Identity.GetUserName());
            List<List<AllFiles>> allFiles = new List<List<AllFiles>>();

            foreach (var item in fileGuids)
            {
                if ((RepositoryFileConfigs.CheckDeleteDate(item)))
                {                 
                    DeleteGroupFiles(item);
                    fileGuids.Remove(item);
                }
            }
            
            foreach (var item in fileGuids)
            {
                allFiles.Add(RepositoryAllFiles.GetFilesByFileGuid(item));
            }

            //Формируем представление требуемых файлов и выводим на экран
            int filesGuidCounter = 0;
            int totalFilesSize = 0;
            int numberOfFiles = 0;

            List<FilesForProfile> filesForProfiles = new List<FilesForProfile>();

            foreach (var item in allFiles)
            {             
                foreach (var item2 in item)
                {
                    totalFilesSize += item2.FileSize;
                    numberOfFiles++;
                }

                filesForProfiles.Add(new FilesForProfile
                {
                    FileGuid = fileGuids[filesGuidCounter],
                    TotalFileSize = GetFileSize(totalFilesSize),
                    NumberOfFiles = numberOfFiles,
                    DownloadLink = Request.Url.Authority + "/" + fileGuids[filesGuidCounter]
                });

                filesGuidCounter++;
                totalFilesSize = 0;
                numberOfFiles = 0;
            }

            return View(filesForProfiles);
        }

        [HttpGet]
        public ActionResult DeleteFiles(string fileGuid)
        {
            DeleteGroupFiles(fileGuid);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ShowAllFiles()
        {
            List<List<AllFiles>> allFiles = new List<List<AllFiles>>();
            List<string> fileGuids = RepositoryGroupFiles.GetAllFileGuids();

            foreach (var item in fileGuids)
            {
                if ((RepositoryFileConfigs.CheckDeleteDate(item)))
                {
                    DeleteGroupFiles(item);
                    fileGuids.Remove(item);
                }
            }

            foreach (var item in fileGuids)
            {
                allFiles.Add(RepositoryAllFiles.GetFilesByFileGuid(item));
            }

            //Формируем представление требуемых файлов и выводим на экран
            int filesGuidCounter = 0;
            int totalFilesSize = 0;
            int numberOfFiles = 0;

            List<FilesForAllFiles> forAllFiles = new List<FilesForAllFiles>();       

            foreach (var item in allFiles)
            {
                foreach (var item2 in item)
                {
                    totalFilesSize += item2.FileSize;
                    numberOfFiles++;
                }

                forAllFiles.Add(new FilesForAllFiles
                {
                    FileGuid = fileGuids[filesGuidCounter],
                    TotalFileSize = GetFileSize(totalFilesSize),
                    NumberOfFiles = numberOfFiles,
                    DownloadLink = Request.Url.Authority + "/" + fileGuids[filesGuidCounter],
                    UserName = RepositoryGroupFiles.GetUserName(fileGuids[filesGuidCounter])           
                });

                filesGuidCounter++;
                totalFilesSize = 0;
                numberOfFiles = 0;
            }

            return View(forAllFiles);
        }

        //public ActionResult ShowSearchedFiles(string fileName)
        //{
        //    List<AllFiles> AllFiles = new List<AllFiles>();
        //    AllFiles = AllFilesRepository.GetFilesByPartOfName(fileName);

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult FileConfigs()
        //{
        //    return View();
        //}
        #region GetFileSize
        /// <summary>
        /// Преобразует байты в килобайты или мегабайты
        /// </summary>
        /// <param name="fileSize">Количество байт</param>
        /// <returns></returns>
        private string GetFileSize(int fileSize)
        {
            const int ONE_MB = 1048576;
            const int ONE_KB = 1024;
            int fileSizeKilobyte = 0;
            int fileSizeMegabyte = 0;

            if (fileSize > ONE_MB)
            {
                fileSizeMegabyte = fileSize / ONE_MB;
                fileSize %= ONE_MB;
            }

            if (fileSize > ONE_KB)
            {
                fileSizeKilobyte = fileSize / ONE_KB;
                fileSize %= ONE_KB;
            }

            if (fileSizeMegabyte == 0 && fileSizeKilobyte == 0)
            {
                return fileSize + " байт";
            }
            else if (fileSizeMegabyte > 0)
            {
                return fileSizeMegabyte + "," + (fileSizeKilobyte / 100) + " мб";
            }
            else
            {
                return fileSizeKilobyte + " кб";
            }
        }
        #endregion

        #region CreateFolderGuid
        /// <summary>
        /// Создает Guid для папки файла/файлов
        /// </summary>
        /// <returns>Возвращает строку из 5 символов</returns>
        private string CreateFolderGuid()
        {
            const int FILE_GUID_LENGTH = 5;
            Guid fileGuidFolder = Guid.NewGuid();
            string shortFileGuid;

            shortFileGuid = fileGuidFolder.ToString();
            shortFileGuid = shortFileGuid.Remove(FILE_GUID_LENGTH);

            foreach (var userFile in RepositoryGroupFiles.GroupFiles)
            {
                if (shortFileGuid == userFile.GroupFileGuid)
                {
                    return null;
                }
            }

            return shortFileGuid;
        }
        #endregion

        #region DeleteFiles
        /// <summary>
        /// Удаляет группу файлов из всего каталога fileGuid
        /// и все соответствующие записи в БД
        /// </summary>
        /// <param name="fileGuid"></param>
        private void DeleteGroupFiles(string fileGuid)
        {
            Directory.Delete(Server.MapPath(@"~/AllUsersFiles/" + fileGuid), true);
            RepositoryFileConfigs.DeleteGroupFiles(fileGuid);
        }
        #endregion

        private string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}