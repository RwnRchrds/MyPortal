using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("AttendanceCodes")]
    public class AttendanceCode : BaseTypes.Entity, ISystemEntity, ICensusEntity, IActivatable
    {
        [Column(Order = 2)]
        [Required]
        [StringLength(1)]
        public string Code { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        [Column(Order = 4)] public Guid AttendanceCodeTypeId { get; set; }

        [Column(Order = 5)] public bool Active { get; set; }

        // Only users with the UseRestrictedCodes permission can use these
        [Column(Order = 6)] public bool Restricted { get; set; }

        [Column(Order = 7)] public bool System { get; set; }

        public virtual AttendanceCodeType CodeType { get; set; }

        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }
    }
}