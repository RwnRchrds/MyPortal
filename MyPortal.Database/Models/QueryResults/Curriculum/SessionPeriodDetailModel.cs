using System;

namespace MyPortal.Database.Models.QueryResults.Curriculum
{
    public class SessionPeriodDetailModel
    {
        public Guid AttendanceWeekId { get; set; }
        public Guid? SessionId { get; set; }
        public Guid PeriodId { get; set; }
        public Guid StudentGroupId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PeriodName { get; set; }
        public string ClassCode { get; set; }
        public Guid? TeacherId { get; set; }
        public string TeacherName { get; set; }
        public Guid? RoomId { get; set; }
        public string RoomName { get; set; }
        public bool IsCover { get; set; }
    }
}
