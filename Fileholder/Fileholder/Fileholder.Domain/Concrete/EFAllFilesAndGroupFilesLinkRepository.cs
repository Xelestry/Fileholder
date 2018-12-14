using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fileholder.Domain.Abstract;
using System.Data.SqlClient;

namespace Fileholder.Domain.Concrete
{
    public class EFAllFilesAndGroupFilesLinkRepository : Test, IAllFilesAndGroupFilesLinkRepository
    {
        public void AddLink(AllFilesAndGroupFilesLink allFilesAndGroupFilesLink)
        {
            context.AllFilesAndGroupFilesLink.Add(allFilesAndGroupFilesLink);
            context.SaveChanges();
        }

    }
}
