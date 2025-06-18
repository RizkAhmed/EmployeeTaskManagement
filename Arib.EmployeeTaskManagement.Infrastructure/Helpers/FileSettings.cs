using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Infrastructure.Helpers
{
    public class FileSettings
    {
        public long MaxFileSize { get; set; }
        public string[] AllowedImageExtensions { get; set; }
        public string[] AllowedFileExtensions { get; set; }
        public string ImageFolderPath { get; set; }
        public string FileFolderPath { get; set; }
    }
}
