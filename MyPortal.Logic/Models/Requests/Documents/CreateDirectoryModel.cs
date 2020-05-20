using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Documents
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
