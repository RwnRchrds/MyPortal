using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AttendanceWeeks")]
    public class AttendanceWeek : BaseTypes.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendanceWeek()
        {
            AttendanceMarks = new HashSet<AttendanceMark>();
        }

        [Column(Order = 2)] public Guid WeekPatternId { get; set; }

        [Column(Order = 3)] public Guid AcademicTermId { get; set; }

        [Column(Order = 4, TypeName = "date")] public DateTime Beginning { get; set; }

        [Column(Order = 5)] public bool IsNonTimetable { get; set; }

        public virtual AcademicTerm AcademicTerm { get; set; }
        public virtual AttendanceWeekPattern WeekPattern { get; set; }

        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }
        public virtual ICollection<ReportCardEntry> ReportCardSubmissions { get; set; }
        public virtual ICollection<SessionExtraName> SessionExtraNames { get; set; }
    }
}