namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A week in the calendar in which attendance marks are recorded. Attendance weeks make up an academic year.
    /// </summary>
    [Table("Attendance_Weeks")]
    public partial class AttendanceWeek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendanceWeek()
        {
            AttendanceMarks = new HashSet<AttendanceMark>();
        }

        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Beginning { get; set; }

        public bool IsHoliday { get; set; }

        public bool IsNonTimetable { get; set; }

        public virtual CurriculumAcademicYear AcademicYear { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceMark> AttendanceMarks { get; set; }
    }
}
