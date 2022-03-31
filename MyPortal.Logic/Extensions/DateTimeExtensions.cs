using System;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Data;

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

        internal static DateTime? GetNextOccurrence(this DateTime dateTime, EventFrequency frequency)
        {
            switch (frequency)
            {
                case EventFrequency.Daily:
                    return dateTime.AddDays(1);
                case EventFrequency.Weekly:
                    return dateTime.AddDays(7);
                case EventFrequency.BiWeekly:
                    return dateTime.AddDays(14);
                case EventFrequency.Monthly:
                    return dateTime.AddMonths(1);
                case EventFrequency.BiMonthly:
                    return dateTime.AddMonths(2);
                case EventFrequency.Annually:
                    return dateTime.AddYears(1);
                case EventFrequency.BiAnnually:
                    return dateTime.AddYears(2);
                default:
                    return null;
            }
        }

        internal static DateTime? GetNextOccurrence(this DateTime dateTime, WeeklyPatternModel weeklyPattern)
        {
            DateTime currentDate = dateTime.AddDays(1);

            while (currentDate <= weeklyPattern.EndDate)
            {
                if (weeklyPattern.Days.Contains(currentDate.DayOfWeek))
                {
                    return currentDate;
                }

                currentDate = currentDate.AddDays(1);
            }

            return null;
        }

        internal static bool IsWeekday(this DateTime dateTime)
        {
            return dateTime.DayOfWeek != DayOfWeek.Saturday && dateTime.DayOfWeek != DayOfWeek.Sunday;
        }

        internal static DateTime GetEndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddTicks(TimeSpan.TicksPerDay - 1);
        }
    }
}