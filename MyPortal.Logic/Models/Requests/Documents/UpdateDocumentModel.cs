using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class UpdateDocumentModel : CreateDocumentModel
    {
        public Guid Id { get; set; }

        public new string FileId { get; set; }
    }
}
