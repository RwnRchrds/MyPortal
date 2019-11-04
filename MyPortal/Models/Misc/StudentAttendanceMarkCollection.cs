using System.Collections.Generic;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Interfaces;

namespace MyPortal.Models.Misc
{
    public class StudentAttendanceMarkCollection : IGridDto
    {
        public string StudentName { get; set; }
        public IEnumerable<AttendanceMarkLite> Marks { get; set; }
    }
}