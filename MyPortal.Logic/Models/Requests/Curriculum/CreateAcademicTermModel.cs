using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class CreateAcademicTermModel
    {
        [StringLength(128)]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CreateAttendanceWeekModel[] AttendanceWeeks { get; set; }
        public DateTime[] Holidays { get; set; }
        public CreateTermWeekPatternModel[] WeekPatterns { get; set; }
    }
}
