using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Requests.Attendance;

namespace MyPortal.Logic.Models.Response.Students
{
    public class StudentStatsModel
    {
        public Guid StudentId { get; set; }
        public int AchievementPoints { get; set; }
        public int BehaviourPoints { get; set; }
        public double? PercentageAttendance { get; set; }
        public int Exclusions { get; set; }
    }
}
