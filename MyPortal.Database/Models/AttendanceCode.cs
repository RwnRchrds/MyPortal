using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("AttendanceCodes")]
    public partial class AttendanceCode : Entity
    {
        [Column(Order = 1)]
        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        [Column(Order = 3)]
        public Guid MeaningId { get; set; }

        [Column(Order = 4)]
        public bool Active { get; set; }

        [Column(Order = 5)]
        public bool Restricted { get; set; }

        public virtual AttendanceCodeMeaning CodeMeaning { get; set; }
    }
}
