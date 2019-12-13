using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class AttendanceWeekDto
    {
        public int Id { get; set; }
        public int AcademicYearId { get; set; }
        public DateTime Beginning { get; set; }
        public bool IsHoliday { get; set; }
        public bool IsNonTimetable { get; set; }
        public virtual AcademicYearDto AcademicYear { get; set; }
    }
}
