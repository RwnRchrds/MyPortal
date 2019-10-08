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

        public int AcademicYearId { get; set; }

        
        public DateTime Beginning { get; set; }

        public bool IsHoliday { get; set; }

        public bool IsNonTimetable { get; set; }

        public virtual CurriculumAcademicYearDto AcademicYear { get; set; }
        
        
        
    }
}
