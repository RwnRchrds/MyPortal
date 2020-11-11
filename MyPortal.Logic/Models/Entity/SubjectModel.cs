using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SubjectModel : BaseModel
    {
        public Guid SubjectCodeId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(5)]
        public string Code { get; set; }

        public bool Deleted { get; set; }

        public virtual SubjectCodeModel SubjectCode { get; set; }
    }
}
