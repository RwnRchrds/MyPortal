using System;
using System.Collections.Generic;
using MyPortal.Logic.Models.List;

namespace MyPortal.Logic.Models.Requests.Attendance
{
    public class AttendanceRegisterStudentModel
    {
        public AttendanceRegisterStudentModel()
        {
            Marks = new List<AttendanceMarkListModel>();
        }

        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public ICollection<AttendanceMarkListModel> Marks { get; set; }
    }
}