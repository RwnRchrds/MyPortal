using System;

namespace MyPortal.Logic.Models.Documents
{
    public class UpdateDocumentModel : CreateDocumentModel
    {
        public Guid Id { get; set; }

        public new string FileId { get; set; }
    }
}
