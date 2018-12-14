using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fileholder.Domain.Abstract
{
    public interface IAllFilesAndGroupFilesLinkRepository
    {
        IQueryable<AllFilesAndGroupFilesLink> AllFilesAndGroupFilesLink { get; }

        void AddLink(AllFilesAndGroupFilesLink allFilesAndGroupFilesLink);
    }
}
