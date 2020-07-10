using System;

namespace MyPortal.Logic.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetDayOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
        {
            var monday = dateTime.Date.AddDays((7 + (dateTime.DayOfWeek - DayOfWeek.Monday) % 7) * -1);

            var diff = dayOfWeek - DayOfWeek.Monday;

            if (diff == -1)
            {
                diff = 6;
            }

            return monday.AddDays(diff);
        }
    }
}