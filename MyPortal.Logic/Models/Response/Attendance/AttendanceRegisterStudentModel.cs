using System;
using System.Collections.Generic;
using MyPortal.Logic.Models.Collection;

namespace MyPortal.Logic.Models.Response.Attendance
{
    public class AttendanceRegisterStudentModel
    {
        public AttendanceRegisterStudentModel()
        {
            Marks = new List<AttendanceMarkCollectionItemModel>();
        }

        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public ICollection<AttendanceMarkCollectionItemModel> Marks { get; set; }
    }
}