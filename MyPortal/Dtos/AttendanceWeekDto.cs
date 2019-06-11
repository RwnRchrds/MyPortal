namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A week in the calendar in which attendance marks are recorded. Attendance weeks make up an academic year.
    /// </summary>
    public partial class AttendanceWeekDto
    {
        public int Id { get; set; }

        [Display(Name="Academic Year")]
        public int AcademicYearId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Beginning { get; set; }

        [Display(Name="Is Holiday")]
        public bool IsHoliday { get; set; }

        [Display(Name="Is Non-TT")]
        public bool IsNonTimetable { get; set; }

        public virtual CurriculumAcademicYearDto CurriculumAcademicYear { get; set; }
    }
}
