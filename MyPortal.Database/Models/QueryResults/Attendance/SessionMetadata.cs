using System;

namespace MyPortal.Database.Models.QueryResults.Attendance
{
    public class SessionMetadata
    {
        // public DateTime FirstDate { get; set; }
        // public DateTime LastDate { get; set; }
        public Guid AttendanceWeekId { get; set; }
        public Guid? SessionId { get; set; }
        public Guid PeriodId { get; set; }
        // public Guid ClassId { get; set; }
        // public Guid? CurriculumGroupId { get; set; }
        public Guid StudentGroupId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PeriodName { get; set; }
        public string ClassCode { get; set; }
        // public string CourseDescription { get; set; }
        public Guid? TeacherId { get; set; }
        public string TeacherName { get; set; }
        public Guid? RoomId { get; set; }
        public string RoomName { get; set; }
        public bool IsCover { get; set; }
    }
}
