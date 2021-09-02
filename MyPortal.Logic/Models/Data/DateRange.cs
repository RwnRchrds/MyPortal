using System;
using System.Collections.Generic;
using MyPortal.Logic.Extensions;

namespace MyPortal.Logic.Models.Data
{
    public class DateRange
    {
        public static DateRange GetCurrentWeek()
        {
            var monday = DateTime.Today.GetDayOfWeek(DayOfWeek.Monday);

            return new DateRange(monday, monday.AddDays(6));
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

        public void Extend(int? days, int? hours = null, int? minutes = null)
        {
            End = End.AddDays(days ?? 0).AddHours(hours ?? 0).AddMinutes(minutes ?? 0);
        }

        public IEnumerable<DateTime> GetAllDates()
        {
            return DateTimeExtensions.GetAllDates(Start, End);
        }

        public Tuple<DateTime, DateTime> ToTuple()
        {
            return new Tuple<DateTime, DateTime>(Start, End);
        }
    }
}