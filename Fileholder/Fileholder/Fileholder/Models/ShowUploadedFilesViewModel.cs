using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fileholder.Models
{
    public class ShowUploadedFilesViewModel
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileGuid { get; set; }
        public string FileSize  { get; set; }
        public DateTime FileUploadTime { get; set; }
        public string UserLogin { get; set; }
        public string TotalSizeAllFiles { get; set; }
        public string FilePassword { get; set; }
        public int NumberOfDownload { get; set; }
        public DateTime FileDeleteTime { get; set; }

        //public ShowUploadedFilesViewModel()
        //{
        //    FileId = new List<int>();
        //    FileName = new List<string>();
        //    FileExtension = new List<string>();
        //    FileSize = new List<string>();
        //}
    }
}