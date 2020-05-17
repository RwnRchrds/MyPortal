using System.Collections.Generic;
using MyPortal.Logic.Models.ListModels;

namespace MyPortal.Logic.Models.Requests.Attendance
{
    public class StudentAttendanceMarkCollection
    {
        public string StudentName { get; set; }
        public IEnumerable<AttendanceMarkListModel> Marks { get; set; }
    }
}