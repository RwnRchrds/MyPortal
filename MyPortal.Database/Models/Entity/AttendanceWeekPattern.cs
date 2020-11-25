using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AttendanceWeekPatterns")]
    public class AttendanceWeekPattern : BaseTypes.Entity
    {
        public AttendanceWeekPattern()
        {
            AttendanceWeeks = new HashSet<AttendanceWeek>();
            Periods = new HashSet<AttendancePeriod>();
        }

        [Column(Order = 1)]
        public Guid AcademicYearId { get; set; }

        [Column(Order = 1)] 
        public int Order { get; set; }

        [Column(Order = 2)] 
        [Required]
        [StringLength(128)]
        public string Description { get; set; }
        
        public virtual AcademicYear AcademicYear { get; set; }
        public virtual ICollection<AttendanceWeek> AttendanceWeeks { get; set; }
        public virtual ICollection<AttendancePeriod> Periods { get; set; }
    }
}
