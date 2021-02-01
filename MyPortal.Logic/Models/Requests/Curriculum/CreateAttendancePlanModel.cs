using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class CreateAttendancePlanModel
    {
        public CreateAttendanceWeekModel[] AttendanceWeeks { get; set; }
        public DateTime[] Holidays { get; set; }
    }
}
