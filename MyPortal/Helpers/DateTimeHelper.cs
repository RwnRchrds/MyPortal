using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime StartOfWeek(this DateTime dt)
        {
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}