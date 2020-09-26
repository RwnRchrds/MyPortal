using System;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class UpdateDocumentModel : CreateDocumentModel
    {
        public Guid Id { get; set; }

        public new string FileId { get; set; }
    }
}
