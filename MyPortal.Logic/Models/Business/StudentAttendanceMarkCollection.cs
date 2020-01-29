using System.Collections.Generic;
using MyPortal.Logic.Models.Lite;

namespace MyPortal.Logic.Models.Business
{
    public class StudentAttendanceMarkCollection
    {
        public string StudentName { get; set; }
        public IEnumerable<AttendanceMarkLiteDto> Marks { get; set; }
    }
}