using System;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class AttendanceWeekRequestModel
    {
        public DateTime WeekBeginning { get; set; }
        public Guid WeekPatternId { get; set; }
        public bool NonTimetable { get; set; }
    }
}