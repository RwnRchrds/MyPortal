using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("AttendanceCodes")]
    public partial class AttendanceCode : BaseTypes.Entity, ICensusEntity, IActivatable
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
        public Guid AttendanceCodeTypeId { get; set; }

        [Column(Order = 4)]
        public bool Active { get; set; }

        // TODO: Add permission for using restricted codes
        [Column(Order = 5)]
        public bool Restricted { get; set; }
        
        [Column(Order = 6)]
        public bool System { get; set; }

        public virtual AttendanceCodeType CodeType { get; set; }

        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }
    }
}
