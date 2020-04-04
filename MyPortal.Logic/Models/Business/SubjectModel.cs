using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Business
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
