using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fileholder.Models
{
    public class FilesForAllFiles
    {
        public string FileGuid { get; set; }
        public string TotalFileSize { get; set; }
        public int NumberOfFiles { get; set; }
        public string DownloadLink { get; set; }
        public string UserName { get; set; }
    }
}