using System;

namespace MyPortal.Processes
{
    public static class DateTimeProcesses
    {
        #region Extension Methods
        public static DateTime StartOfWeek(this DateTime dt)
        {
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        #endregion
    }
}