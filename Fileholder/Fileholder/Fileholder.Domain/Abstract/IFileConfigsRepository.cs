using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fileholder.Domain.Abstract
{
    public interface IFileConfigsRepository
    {
        IQueryable<FileConfigs> FileConfigs { get; }

        void AddConfig(FileConfigs fileConfigs);
        List<FileConfigs> GetAllFilesConfigs();
        bool CheckDeleteDate(string fileGuid);
        void DeleteGroupFiles(string fileGuid);
        void IncreaseDownloadCount(string fileGuid);
        FileConfigs GetConfigsByFileGuid(string fileGuid);
        Task SetPasswordOnFiles(string pass, string fileGuid);
    }
}
