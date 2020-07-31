using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("AttendanceCodeMeanings")]
    public class AttendanceCodeMeaning : Entity
    {
        public AttendanceCodeMeaning()
        {
            Codes = new HashSet<AttendanceCode>();
        }

        [Column(Order = 1)]
        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<AttendanceCode> Codes { get; set; }
    }
}
