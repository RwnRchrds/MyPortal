using System.Collections.Generic;
using MyPortal.Dtos.LiteDtos;

namespace MyPortal.Models.Misc
{
    public class StudentAttendanceMarkCollection
    {
        public string StudentName { get; set; }
        public IEnumerable<AttendanceMarkLite> Marks { get; set; }
    }
}