using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyPortal.Logic.Models.DocumentProvision
{
    public class FileUpload
    {
        public FileMetadata Metadata { get; set; }
        public Stream Stream { get; set; }
    }
}
