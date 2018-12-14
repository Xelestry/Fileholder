using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fileholder.Models
{
    public class FilesForProfile
    {
        public string FileGuid { get; set; }
        public string TotalFileSize { get; set; }
        public int NumberOfFiles { get; set; }
        public string DownloadLink { get; set; }
    }
}