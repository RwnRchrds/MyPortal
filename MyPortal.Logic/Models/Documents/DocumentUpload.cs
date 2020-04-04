using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Models.Documents
{
    public class DocumentUpload
    {
        public DocumentModel Details { get; set; }
        public FileStream File { get; set; }
    }
}
