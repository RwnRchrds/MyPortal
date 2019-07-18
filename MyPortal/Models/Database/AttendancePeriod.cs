namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// [SYSTEM] Timetable period definitions for each week.
    /// </summary>
    [Table("Attendance_Periods")]
    public partial class AttendancePeriod
    {
        //THIS IS A SYSTEM CLASS AND SHOULD NOT HAVE FEATURES TO ADD, MODIFY OR DELETE DATABASE OBJECTS

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendancePeriod()
        {
            AttendanceRegisterMarks = new HashSet<AttendanceRegisterMark>();
            CurriculumClassPeriods = new HashSet<CurriculumSession>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string Weekday { get; set; }

        public string Name { get; set; }

        [Display(Name="Start Time")]
        public TimeSpan StartTime { get; set; }

        [Display(Name="End Time")]
        public TimeSpan EndTime { get; set; }

        public bool IsAm { get; set; }

        public bool IsPm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceRegisterMark> AttendanceRegisterMarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumSession> CurriculumClassPeriods { get; set; }
    }
}
