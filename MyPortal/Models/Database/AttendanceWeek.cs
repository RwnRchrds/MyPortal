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
        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Beginning { get; set; }

        public bool IsHoliday { get; set; }

        public virtual CurriculumAcademicYear CurriculumAcademicYear { get; set; }
    }
}
