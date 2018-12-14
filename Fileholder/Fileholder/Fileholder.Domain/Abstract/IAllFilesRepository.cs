using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fileholder.Domain.Abstract
{
    public interface IAllFilesRepository
    {
        IQueryable<AllFiles> AllFiles { get; }

        void AddFile(AllFiles allFiles);
        List<AllFiles> GetFilesByFileGuid(string fileGuidFolder);
        AllFiles GetFileById(int fileId);
        //List<AllFiles> GetFilesByPartOfName(string fileName);
        //List<AllFiles> GetAllFiles();
    }
}
