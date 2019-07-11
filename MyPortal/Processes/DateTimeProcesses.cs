using System;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class DateTimeProcesses
    {
        public static DateTime StartOfWeek(this DateTime dt)
        {
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static bool IsBetween(this DateTime dt, DateTime start, DateTime end)
        {
            if (dt >= start && dt <= end)
            {
                return true;
            }

            return false;
        }

        public static ProcessResponse<DateTime> GetDateTimeFromFormattedInt(int formattedDate)
        {
            int year = formattedDate / 10000;
            int month = ((formattedDate - (10000 * year)) / 100);
            int day = (formattedDate - (10000 * year) - (100 * month));

            return new ProcessResponse<DateTime>(ResponseType.Ok, null, new DateTime(year, month, day));
        }
    }
}