namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendance_Weeks")]
    public partial class AttendanceWeek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendanceWeek()
        {
            AttendanceRegisterMarks = new HashSet<AttendanceRegisterMark>();
        }
        public int Id { get; set; }

        [Display(Name="Academic Year")]
        public int AcademicYearId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Beginning { get; set; }

        [Display(Name="Is Holiday")]
        public bool IsHoliday { get; set; }

        [Display(Name="Is Non-TT")]
        public bool IsNonTimetable { get; set; }

        public virtual CurriculumAcademicYear CurriculumAcademicYear { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceRegisterMark> AttendanceRegisterMarks { get; set; }
    }
}
