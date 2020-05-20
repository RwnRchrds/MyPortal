using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class CreateDocumentModel
    {
        public Guid TypeId { get; set; }
        public Guid DirectoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileId { get; set; }
        public bool Public { get; set; }
    }
}
