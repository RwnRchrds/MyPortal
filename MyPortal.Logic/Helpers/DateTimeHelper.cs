using System;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Logic.Enums;

namespace MyPortal.Logic.Helpers
{
    internal static class DateTimeHelper
    {
        internal static IEnumerable<DateTime> GetAllInstances(DateTime startDate, DateTime endDate,
            DateTimeDivision division = DateTimeDivision.Day, int incrementBy = 1)
        {
            var instances = new List<DateTime>();

            Func<DateTime, DateTime> increment;

            switch (division)
            {
                case DateTimeDivision.Year:
                    increment = time => time.AddYears(incrementBy);
                    break;
                case DateTimeDivision.Month:
                    increment = time => time.AddMonths(incrementBy);
                    break;
                case DateTimeDivision.Day:
                    increment = time => time.AddDays(incrementBy);
                    break;
                case DateTimeDivision.Hour:
                    increment = time => time.AddHours(incrementBy);
                    break;
                case DateTimeDivision.Minute:
                    increment = time => time.AddMinutes(incrementBy);
                    break;
                case DateTimeDivision.Second:
                    increment = time => time.AddSeconds(incrementBy);
                    break;
                case DateTimeDivision.Millisecond:
                    increment = time => time.AddMilliseconds(incrementBy);
                    break;
                default:
                    increment = time => time.AddTicks(incrementBy);
                    break;
            }

            for (var dt = startDate; dt <= endDate; dt = increment.Invoke(dt))
            {
                instances.Add(dt);
            }

            return instances;
        }
    }
}