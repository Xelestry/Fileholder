//TODO: удалить, после рефакторинга
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Fileholder.Domain.Abstract;
//using Fileholder.Domain.Entities;

//namespace Fileholder.Domain.Concrete
//{
//    public class EFUserFilesRepository : IAllFilesRepository
//    {
//        private EFDbContext context = new EFDbContext();

//        public IQueryable<UserFiles> UserFiles
//        {
//            get { return context.UserFiles; }
//        }

//        public void AddFile(UserFiles userFiles)
//        {
//            context.UserFiles.Add(userFiles);
//            context.SaveChanges();                
//        }

//        public List<UserFiles> GetFileByFileGuid(string fileGuidFolder)
//        {
//            List<UserFiles> userFilesByFolder = new List<UserFiles>();

//            foreach (var item in context.UserFiles.Where(uf => uf.FileGuidFolder == fileGuidFolder))
//            {
//                userFilesByFolder.Add(item);
//            }
            
//            return userFilesByFolder;
//        }

//        public UserFiles GetFileById(int fileId)
//        {
//            UserFiles userFiles = context.UserFiles.Find(fileId);
//            return userFiles;
//        }   
        
//        public List<UserFiles> GetFilesByPartOfName(string fileName)
//        {
//            List<UserFiles> userFilesByName = new List<UserFiles>();

//            foreach (var item in context.UserFiles)
//            {
//                if (item.FileName.ToLower().Contains(fileName.ToLower()))
//                {
//                    userFilesByName.Add(item);
//                }
//            }

//            return userFilesByName;
//        }

//        public List<UserFiles> GetAllFiles()
//        {
//            List<UserFiles> userFiles = new List<UserFiles>();

//            foreach (var item in context.UserFiles)
//            {
//                userFiles.Add(item);
//            }

//            return userFiles;
//        }
//    }
//}
