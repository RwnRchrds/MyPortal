using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class DirectoryRequestModel
    {
        [NotEmpty]
        public Guid ParentId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public bool Private { get; set; }
    }
}
