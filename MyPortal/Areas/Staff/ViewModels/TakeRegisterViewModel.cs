using System;
using System.Collections.Generic;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class TakeRegisterViewModel
    {
        public AttendanceWeek Week { get; set; }
        public DateTime SessionDate { get; set; }
        public CurriculumSession Session { get; set; }
        public IEnumerable<AttendancePeriod> Periods { get; set; }
        public IEnumerable<StudentAttendanceMarkCollection> AttendanceMarks { get; set; }
        public IEnumerable<AttendanceCode> AttendanceCodes { get; set; }
        public List<string> UsableCodes { get; set; }
    }
}