using System;

namespace MyPortal.Dtos
{
    public class AttendanceWeekDto
    {
        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        public DateTime Beginning { get; set; }

        public bool IsHoliday { get; set; }

        public CurriculumAcademicYearDto CurriculumAcademicYear { get; set; }
    }
}