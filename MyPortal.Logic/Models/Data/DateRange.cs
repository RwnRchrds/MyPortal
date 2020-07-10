using System;

namespace MyPortal.Logic.Models.Data
{
    public class DateRange
    {
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
            End.AddDays(days ?? 0).AddHours(hours ?? 0).AddMinutes(minutes ?? 0);
        }

        public Tuple<DateTime, DateTime> ToTuple()
        {
            return new Tuple<DateTime, DateTime>(Start, End);
        }
    }
}