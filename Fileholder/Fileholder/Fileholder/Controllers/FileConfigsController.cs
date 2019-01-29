using System.Collections.Generic;
using System.Web.Mvc;
using Fileholder.Domain.Abstract;
using System;
using Fileholder.Domain;
using System.Timers;
using Quartz;
using System.Net.Mail;
using System.Threading.Tasks;
using Quartz.Impl;
using Fileholder.Models;
using System.Web.Services;
using System.Web.Script.Services;

namespace Fileholder.Controllers
{
    public class FileConfigsController : Controller
    {
        #region Repository
        private IAllFilesRepository RepositoryAllFiles;
        private IGroupFilesOfOneUploadRepository RepositoryGroupFiles;
        private IAllFilesAndGroupFilesLinkRepository RepositoryLinkFiles;
        private IFileConfigsRepository RepositoryFileConfigs;

        public FileConfigsController(
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

        public FileConfigsController(GroupFileOfOneUpload groupFiles, IFileConfigsRepository fileConfigs)
        {
            RepositoryFileConfigs = fileConfigs;
            DefaultConfigs(groupFiles);
        }
        // GET: FileConfigs
        public void DefaultConfigs(GroupFileOfOneUpload groupFiles)
        {
            const int FILE_LIFE_TIME = 5;
            FileConfigs fileConfigs = new FileConfigs
            {
                GroupFileId = groupFiles.GroupFileId,
                DateOfDeletion = groupFiles.FileUploadDate.AddDays(FILE_LIFE_TIME),
                DownloadCounter = 0
            };

            RepositoryFileConfigs.AddConfig(fileConfigs);
        }

        public List<FileConfigs> GetAllFileConfigs()
        {
            return RepositoryFileConfigs.GetAllFilesConfigs();
        }

        public void SetPassword(string pass, string fileGuid)
        {
            RepositoryFileConfigs.SetPasswordOnFiles(pass, fileGuid);
        }

        public ActionResult CheckPassForFile(string pass, string fileGuid)
        {
            var fileConfig = RepositoryFileConfigs.GetConfigsByFileGuid(fileGuid);

            if(fileConfig.Password == pass)
            {
                return View();
            }

            return View();
        }
    }

}