using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceCodeModel : BaseModel
    {
        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public Guid MeaningId { get; set; }

        public bool Active { get; set; }

        public bool Restricted { get; set; }

        public virtual AttendanceCodeMeaningModel CodeMeaning { get; set; }
    }
}
