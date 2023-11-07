using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class AcademicTermRequestModel
    {
        [StringLength(128)] public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public AttendanceWeekRequestModel[] AttendanceWeeks { get; set; }
        public DateTime[] Holidays { get; set; }
        public CreateTermWeekPatternRequestModel[] WeekPatterns { get; set; }
    }
}