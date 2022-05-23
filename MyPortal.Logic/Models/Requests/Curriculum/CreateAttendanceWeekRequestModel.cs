using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class CreateAttendanceWeekRequestModel
    {
        public DateTime WeekBeginning { get; set; }
        public Guid WeekPatternId { get; set; }
        public bool NonTimetable { get; set; }
    }
}
