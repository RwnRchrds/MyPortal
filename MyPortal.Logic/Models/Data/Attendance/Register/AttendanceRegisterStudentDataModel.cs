using System;
using System.Collections.Generic;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Models.Data.Attendance.Register
{
    public class AttendanceRegisterStudentDataModel
    {
        public AttendanceRegisterStudentDataModel()
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