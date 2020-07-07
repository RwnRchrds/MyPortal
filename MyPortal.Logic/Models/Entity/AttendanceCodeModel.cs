using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceCodeModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public Guid MeaningId { get; set; }

        public bool Active { get; set; }

        public bool Statutory { get; set; }

        public virtual AttendanceCodeMeaningModel CodeMeaning { get; set; }
    }
}
