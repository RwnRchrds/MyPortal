using System;
using System.Collections.Generic;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Models.Data
{
    public class DateRange
    {
        public static DateRange CurrentWeek
        {
            get
            {
                var monday = DateTime.Today.GetDayOfWeek(DayOfWeek.Monday);

                return new DateRange(monday, monday.AddDays(6));
            }
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public TimeSpan GetTotalTime()
        {
            return End - Start;
        }

        public bool Overlaps(DateRange dateRange)
        {
            return Start <= dateRange.End && End >= dateRange.Start;
        }

        public void Extend(int? years, int? months, int? days, int? hours = null, int? minutes = null)
        {
            End = End.AddYears(years ?? 0).AddMonths(months ?? 0).AddDays(days ?? 0).AddHours(hours ?? 0)
                .AddMinutes(minutes ?? 0);
        }

        public IEnumerable<DateTime> GetAllDates()
        {
            return DateTimeHelper.GetAllInstances(Start, End);
        }

        public Tuple<DateTime, DateTime> ToTuple()
        {
            return new Tuple<DateTime, DateTime>(Start, End);
        }
    }
}