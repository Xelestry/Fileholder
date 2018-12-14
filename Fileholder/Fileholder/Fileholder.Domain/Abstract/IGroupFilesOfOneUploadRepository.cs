using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fileholder.Domain.Abstract
{
    public interface IGroupFilesOfOneUploadRepository
    {
        IQueryable<GroupFileOfOneUpload> GroupFiles { get; }

        void AddGroup(GroupFileOfOneUpload groupFiles);
        DateTime GetUploadDate(string fileGuid);
        string GetUserName(string fileGuid);
        string GetFileGuidById(int fileId);
        List<string> GetAllUserFile(string username);
        List<string> GetAllFileGuids();
    }
}
