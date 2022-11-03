using System;
using System.Collections.Generic;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Models.Response.Attendance.Register
{
    public class AttendanceRegisterStudentModel
    {
        public AttendanceRegisterStudentModel()
        {
            Marks = new List<AttendanceMarkSummaryModel>();
        }

        public Guid StudentId { get; set; }
        public Guid? ReportCardId { get; set; }
        public bool HasDetention { get; set; }
        public string StudentName { get; set; }
        public ICollection<AttendanceMarkSummaryModel> Marks { get; set; }
    }
}