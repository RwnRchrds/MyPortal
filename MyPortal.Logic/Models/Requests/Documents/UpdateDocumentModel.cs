using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class UpdateDocumentModel
    {
        public Guid Id { get; set; }

        [NotEmpty]
        public Guid TypeId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Restricted { get; set; }
    }
}
