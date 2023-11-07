using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AttendancePeriods")]
    public class AttendancePeriod : BaseTypes.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendancePeriod()
        {
            AttendanceMarks = new HashSet<AttendanceMark>();
            SessionPeriods = new HashSet<SessionPeriod>();
        }

        [Column(Order = 2)] public Guid WeekPatternId { get; set; }

        [Column(Order = 3)] public DayOfWeek Weekday { get; set; }

        [Column(Order = 4)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 5, TypeName = "time(2)")]
        public TimeSpan StartTime { get; set; }

        [Column(Order = 6, TypeName = "time(2)")]
        public TimeSpan EndTime { get; set; }

        [Column(Order = 7)] public bool AmReg { get; set; }

        [Column(Order = 8)] public bool PmReg { get; set; }

        public virtual AttendanceWeekPattern WeekPattern { get; set; }

        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }

        public virtual ICollection<ReportCardEntry> ReportCardSubmissions { get; set; }

        public virtual ICollection<SessionPeriod> SessionPeriods { get; set; }
    }
}