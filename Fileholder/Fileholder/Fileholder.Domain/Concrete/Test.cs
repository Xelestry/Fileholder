using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fileholder.Domain.Abstract;
using System.Data.SqlClient;

namespace Fileholder.Domain.Concrete
{
    //TODO: изменить класс
    public class Test
    {
        protected EFDbContext context = new EFDbContext();

        public IQueryable<AllFilesAndGroupFilesLink> AllFilesAndGroupFilesLink
        {
            get { return context.AllFilesAndGroupFilesLink; }
        }

        public IQueryable<AllFiles> AllFiles
        {
            get { return context.AllFiles; }
        }

        public IQueryable<GroupFileOfOneUpload> GroupFiles
        {
            get { return context.GroupFilesOfOneUpload; }
        }

        public IQueryable<FileConfigs> FileConfigs
        {
            get { return context.FileConfigs; }
        }
    }
}
