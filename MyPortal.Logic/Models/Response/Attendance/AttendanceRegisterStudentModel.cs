using System;
using System.Collections.Generic;
using MyPortal.Logic.Models.Collection;

namespace MyPortal.Logic.Models.Response.Attendance
{
    public class AttendanceRegisterStudentModel
    {
        public AttendanceRegisterStudentModel()
        {
            Marks = new List<AttendanceMarkCollectionModel>();
        }

        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public ICollection<AttendanceMarkCollectionModel> Marks { get; set; }
    }
}