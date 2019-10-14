using System;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class DateTimeProcesses
    {
        public static DateTime GetDateTimeFromFormattedInt(int formattedDate)
        {
            var year = formattedDate / 10000;
            var month = ((formattedDate - (10000 * year)) / 100);
            var day = (formattedDate - (10000 * year) - (100 * month));

            return new DateTime(year, month, day);
        }

        public static DateTime GetDayOfWeek(this DateTime dt, DayOfWeek day)
        {
            var monday = dt.StartOfWeek();

            switch (day)
            {
                case DayOfWeek.Tuesday:
                    return monday.AddDays(1).Date;

                case DayOfWeek.Wednesday:
                    return monday.AddDays(2).Date;

                case DayOfWeek.Thursday:
                    return monday.AddDays(3).Date;

                case DayOfWeek.Friday:
                    return monday.AddDays(4).Date;

                case DayOfWeek.Saturday:
                    return monday.AddDays(5).Date;

                case DayOfWeek.Sunday:
                    return monday.AddDays(6).Date;
            }

            return monday;
        }

        public static bool IsBetween(this DateTime dt, DateTime start, DateTime end)
        {
            if (dt >= start && dt <= end)
            {
                return true;
            }

            return false;
        }

        public static DateTime StartOfWeek(this DateTime dt)
        {
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        public static string ToDisplayString(this DateTime dt)
        {
            return dt.ToString("dd-MMM-yyyy");
        }
    }
}