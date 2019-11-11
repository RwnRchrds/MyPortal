using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.Extensions
{
    public static class AttendanceExtensions
    {
        public static string GetTimeDisplay(this AttendancePeriod period)
        {
            var time = $"{period.StartTime.ToString(@"hh\:mm")} - {period.EndTime.ToString(@"hh\:mm")}";

            return time;
        }
    }
}