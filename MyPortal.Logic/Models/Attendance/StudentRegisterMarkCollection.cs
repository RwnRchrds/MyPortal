using System.Collections.Generic;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.List;

namespace MyPortal.Logic.Models.Attendance
{
    public class StudentRegisterMarkCollection
    {
        public string StudentName { get; set; }
        public IEnumerable<AttendanceMarkListModel> Marks { get; set; }
    }
}