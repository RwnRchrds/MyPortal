namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendance_Periods")]
    public partial class AttendancePeriod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendancePeriod()
        {
            AttendanceMarks = new HashSet<AttendanceMark>();
            CurriculumClassPeriods = new HashSet<CurriculumClassPeriod>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string Weekday { get; set; }

        public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsAm { get; set; }

        public bool IsPm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumClassPeriod> CurriculumClassPeriods { get; set; }
    }
}
