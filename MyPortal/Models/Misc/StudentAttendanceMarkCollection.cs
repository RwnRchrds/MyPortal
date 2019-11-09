using System.Collections.Generic;
using MyPortal.Dtos.Lite;
using MyPortal.Interfaces;

namespace MyPortal.Models.Misc
{
    public class StudentAttendanceMarkCollection : IGridDto
    {
        public string StudentName { get; set; }
        public IEnumerable<AttendanceMarkLiteDto> Marks { get; set; }
    }
}