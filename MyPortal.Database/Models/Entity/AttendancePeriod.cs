using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AttendancePeriods")]
    public partial class AttendancePeriod : BaseTypes.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendancePeriod()
        {
            AttendanceMarks = new HashSet<AttendanceMark>();
            Sessions = new HashSet<Session>();
        }

        [Column(Order = 1)]
        public Guid WeekPatternId { get; set; }

        [Column(Order = 2)]
        public DayOfWeek Weekday { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        [Column(Order = 4, TypeName = "time(2)")]
        public TimeSpan StartTime { get; set; }
        
        [Column(Order = 5, TypeName = "time(2)")]
        public TimeSpan EndTime { get; set; }

        [Column(Order = 6)]
        public bool AmReg { get; set; }

        [Column(Order = 7)]
        public bool PmReg { get; set; }

        public virtual AttendanceWeekPattern WeekPattern { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }

        public virtual ICollection<ReportCardSubmission> ReportCardSubmissions { get; set; }
    }
}
