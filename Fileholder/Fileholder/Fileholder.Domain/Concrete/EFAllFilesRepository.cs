using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fileholder.Domain.Abstract;
using System.Data.SqlClient;


namespace Fileholder.Domain.Concrete
{
    public class EFAllFilesRepository : Test, IAllFilesRepository
    {
        public void AddFile(AllFiles allFiles)
        {
            context.AllFiles.Add(allFiles);
            context.SaveChanges();
        }

        public List<AllFiles> GetFilesByFileGuid(string fileGuidFolder)
        {
            List<AllFiles> allFiles = new List<AllFiles>();
            
            using (EFDbContext context = new EFDbContext())
            {
                if (!(context.GroupFilesOfOneUpload.Any(a => a.GroupFileGuid == fileGuidFolder)))
                {
                    return null;
                }

                SqlParameter param = new SqlParameter("@file_guid", fileGuidFolder);

                var files = context.Database.SqlQuery<AllFiles>("SELECT * FROM GetFileByFileGuid (@file_guid)", param);

                foreach (var item in files)
                {
                    allFiles.Add(item);
                }
            }

            return allFiles;
        }

        public AllFiles GetFileById(int fileId)
        {
            return context.AllFiles.Find(fileId);
        }
        //List<AllFiles> GetFilesByPartOfName(string fileName);
        //List<AllFiles> GetAllFiles();
    }
}
