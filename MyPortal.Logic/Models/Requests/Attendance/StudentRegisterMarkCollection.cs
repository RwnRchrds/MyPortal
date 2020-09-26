using System.Collections.Generic;
using MyPortal.Logic.Models.DataGrid;

namespace MyPortal.Logic.Models.Requests.Attendance
{
    public class StudentRegisterMarkCollection
    {
        public string StudentName { get; set; }
        public IEnumerable<AttendanceMarkListModel> Marks { get; set; }
    }
}