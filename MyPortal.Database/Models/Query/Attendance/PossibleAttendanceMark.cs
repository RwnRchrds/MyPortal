using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Models.Query.Attendance
{
    public class PossibleAttendanceMark
    {
        public Guid? Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid WeekId { get; set; }
        public Guid PeriodId { get; set; }
        public Guid? CodeId { get; set; }
        public string Comments { get; set; }
        public int MinutesLate { get; set; }
    }
}
