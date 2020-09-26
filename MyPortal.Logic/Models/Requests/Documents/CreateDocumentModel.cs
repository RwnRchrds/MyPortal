using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class CreateDocumentModel
    {
        [NotEmpty]
        public Guid TypeId { get; set; }

        [NotEmpty]
        public Guid DirectoryId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Restricted { get; set; }
    }
}
