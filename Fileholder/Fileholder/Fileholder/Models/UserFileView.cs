using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fileholder.Models
{
    public class UserFileView
    {
        public string FileGuidFolder { get; set; }
        public List<string> FileNames { get; set; }
        public int NumberOfFiles { get; set; }
        public string TotalFileSize { get; set; }
        public int NumberOfDownloads { get; set; }
        public string UserName { get; set; }
        public int UploadDate { get; set; }
    }
}