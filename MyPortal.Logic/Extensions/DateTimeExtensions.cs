using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPortal.Logic.Extensions
{
    internal static class DateTimeExtensions
    {
        internal static DateTime GetDayOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            int diff = (7 + (dateTime.DayOfWeek - DayOfWeek.Monday)) % 7;

            var monday = dateTime.AddDays(-1 * diff).Date;

            var counter = dayOfWeek - DayOfWeek.Monday;

            if (counter == -1)
            {
                counter = 6;
            }

            return monday.AddDays(counter);
        }

        internal static IEnumerable<DateTime> GetAllDates(DateTime startDate, DateTime endDate)
        {
            var dates = new List<DateTime>();

            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            return dates.OrderBy(x => x).ToList();
        }

        internal static bool IsWeekday(this DateTime dateTime)
        {
            return dateTime.DayOfWeek != DayOfWeek.Saturday && dateTime.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}