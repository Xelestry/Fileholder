using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using Fileholder.Domain.Abstract;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System;
using System.Web;
using Fileholder.Domain;
using Fileholder.Models;
using Microsoft.AspNet.Identity;


namespace Fileholder.Controllers
{
    public class DownloadFilesController : Controller
    {       
        #region Repository
        private IAllFilesRepository RepositoryAllFiles;
        private IGroupFilesOfOneUploadRepository RepositoryGroupFiles;
        private IAllFilesAndGroupFilesLinkRepository RepositoryLinkFiles;
        private IFileConfigsRepository RepositoryFileConfigs;

        public DownloadFilesController(
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


        [HttpGet]
        public ActionResult DownloadOneFile(int fileId)
        {
            AllFiles userFiles = RepositoryAllFiles.GetFileById(fileId);

            return DownloadFile(userFiles);
        }

        //TODO:добавить сюда счетчик скачиваний
        [HttpGet]
        public ActionResult DownloadAllFiles(string fileGuid)
        {
            List<AllFiles> userFiles = RepositoryAllFiles.GetFilesByFileGuid(fileGuid);           

            string zipFilename = Request.Url.Authority + @"/" + fileGuid + ".zip";

            MemoryStream outputMemStream = new MemoryStream();
            ZipOutputStream zipStream = new ZipOutputStream(outputMemStream);

            zipStream.SetLevel(3); // уровень сжатия от 0 до 9

            foreach (var file in userFiles)
            {
                FileInfo fi = new FileInfo(Server.MapPath("~/AllUsersFiles/" +
                    fileGuid + @"/" + file.FileId + @"/" + file.FileName));

                string entryName = ZipEntry.CleanName(fi.Name);
                ZipEntry newEntry = new ZipEntry(entryName)
                {
                    DateTime = fi.LastWriteTime,
                    Size = fi.Length
                };
                zipStream.PutNextEntry(newEntry);
                
                byte[] buffer = new byte[4096];
                using (FileStream streamReader = System.IO.File.OpenRead(fi.FullName))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
            zipStream.IsStreamOwner = false;
            zipStream.Close();

            outputMemStream.Position = 0;

            string file_type = "application/zip";

            //увеличиваем кол-во скачиваний
            RepositoryFileConfigs.IncreaseDownloadCount(fileGuid);

            return File(outputMemStream, file_type, zipFilename);
        }

        private FileResult DownloadFile(AllFiles userFiles)
        {
            //AllFiles userFile = RepositoryAllFiles.AllFiles.First(uf => uf.FileId == userFiles.FileId);
            string path = Server.MapPath("~/AllUsersFiles/") +
                RepositoryGroupFiles.GetFileGuidById(userFiles.FileId) + @"/" + userFiles.FileId + @"/" + userFiles.FileName;

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);

            return File(fileBytes, userFiles.FileExtension, userFiles.FileName);
        }
    }
}