using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos.Lite;

namespace MyPortal.BusinessLogic.Models.Data
{
    public class StudentAttendanceMarkCollection
    {
        public string StudentName { get; set; }
        public IEnumerable<AttendanceMarkLiteDto> Marks { get; set; }
    }
}