using System;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Requests.Calendar;

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

        internal static DateTime? GetNextOccurrence(this DateTime dateTime, RecurringRequestModel recurringModel)
        {
            if (recurringModel.Frequency == EventFrequency.Daily && recurringModel.Days != null && recurringModel.LastOccurrence.HasValue)
            {
                var weeklyPattern = new WeeklyPatternModel(recurringModel.Days, recurringModel.LastOccurrence.Value);

                return GetNextOccurrence(dateTime, weeklyPattern);
            }

            return GetNextOccurrence(dateTime, recurringModel.Frequency);
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

        internal static bool IsWeekend(this DateTime dateTime)
        {
            return dateTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
        }

        internal static DateTime GetEndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddTicks(TimeSpan.TicksPerDay - 1);
        }
    }
}