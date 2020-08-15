using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Documents
{
    public class CreateDirectoryModel
    {
        [NotEmpty]
        public Guid ParentId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public bool Private { get; set; }
        public bool StaffOnly { get; set; }
    }
}
