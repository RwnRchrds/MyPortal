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
            return $"{period.StartTime:hh:mm} - {period.EndTime:hh:mm}";
        }
    }
}