using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class AttendanceWeekDto
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public DateTime Beginning { get; set; }

        public bool IsHoliday { get; set; }

        public bool IsNonTimetable { get; set; }

        public virtual AcademicYearDto AcademicYear { get; set; }
    }
}
