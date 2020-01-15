using System;
using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Models.Data;

namespace MyPortal.Areas.Staff.ViewModels.Attendance
{
    public class TakeRegisterViewModel
    {
        public AttendanceWeekDto Week { get; set; }
        public DateTime SessionDate { get; set; }
        public SessionDto Session { get; set; }
        public IEnumerable<PeriodDto> Periods { get; set; }
        public IEnumerable<StudentAttendanceMarkCollection> AttendanceMarks { get; set; }
        public IEnumerable<AttendanceCodeDto> AttendanceCodes { get; set; }
        public List<string> UsableCodes { get; set; }
    }
}