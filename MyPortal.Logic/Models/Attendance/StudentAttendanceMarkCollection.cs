using System.Collections.Generic;
using MyPortal.Logic.Models.Lite;

namespace MyPortal.Logic.Models.Attendance
{
    public class StudentAttendanceMarkCollection
    {
        public string StudentName { get; set; }
        public IEnumerable<AttendanceMarkLiteDto> Marks { get; set; }
    }
}