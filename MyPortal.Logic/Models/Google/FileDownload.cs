using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Routing.Constraints;

namespace MyPortal.Logic.Models.Google
{
    public class FileDownload
    {
        public FileDownload(Stream fileStream, string contentType, string fileName)
        {
            FileStream = fileStream;
            ContentType = contentType;
            FileName = fileName;
        }

        public Stream FileStream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
