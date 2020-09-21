using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("AttendanceWeeks")]
    public partial class AttendanceWeek : Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendanceWeek()
        {
            AttendanceMarks = new HashSet<AttendanceMark>();
        }

        [Column(Order = 2)]
        public Guid WeekPatternId { get; set; }

        [Column(Order = 3, TypeName = "date")]
        public DateTime Beginning { get; set; }

        [Column(Order = 4)]
        public bool IsNonTimetable { get; set; }

        public virtual AttendanceWeekPattern WeekPattern { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }

        public virtual ICollection<ReportCardSubmission> ReportCardSubmissions { get; set; }
    }
}
