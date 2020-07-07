using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class SubjectModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        public bool Deleted { get; set; }
    }
}
