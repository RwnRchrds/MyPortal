using System;

namespace MyPortal.Database.Models.QueryResults.Attendance
{
    public class AttendanceMarkDetailModel
    {
        public Guid AttendanceMarkId { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public Guid WeekId { get; set; }
        public Guid PeriodId { get; set; }
        public Guid CodeId { get; set; }
        public Guid CreatedById { get; set; }
        public string Comments { get; set; }
        public int MinutesLate { get; set; }
    }
}
