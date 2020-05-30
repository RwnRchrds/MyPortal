using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Business
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

        public bool DoNotUse { get; set; }

        public virtual AttendanceCodeMeaningModel CodeMeaning { get; set; }
    }
}
