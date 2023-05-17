using System;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using Org.BouncyCastle.Asn1.X509;

namespace MyPortal.Logic.Models.Structures
{
    public class DateRange
    {
        private DateRange _beforeStart;
        private DateRange _afterEnd;

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

        public DateRange BeforeStart
        {
            get { return _beforeStart; }
        }

        public DateRange AfterEnd
        {
            get { return _afterEnd; }
        }

        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public TimeSpan TotalTime => End - Start;

        public DateRange[] GetAll()
        {
            var ranges = new List<DateRange>();
            ranges.Add(this);
            ranges.AddRange(GetLeft());
            ranges.AddRange(GetRight());

            return ranges.ToArray();
        }

        private DateRange[] GetLeft()
        {
            var ranges = new List<DateRange>();
            
            if (BeforeStart != null)
            {
                ranges.Add(BeforeStart);
                ranges.AddRange(BeforeStart.GetLeft());
            }

            return ranges.ToArray();
        }

        private DateRange[] GetRight()
        {
            var ranges = new List<DateRange>();

            if (AfterEnd != null)
            {
                ranges.Add(AfterEnd);
                ranges.AddRange(AfterEnd.GetRight());
            }

            return ranges.ToArray();
        }

        public bool Overlaps(DateRange dateRange, bool includeAdjacent)
        {
            var overlaps = (Start < dateRange.End && End > dateRange.Start) ||
                           (includeAdjacent && IsAdjacentTo(dateRange));
            
            return overlaps;
        }

        public bool IsAdjacentTo(DateRange dateRange)
        {
            return IsAdjacentToStart(dateRange) || IsAdjacentToEnd(dateRange);
        }

        public bool TryCoalesce(DateRange dateRange)
        {
            try
            {
                Coalesce(dateRange);

                return true;
            }
            catch (ArgumentException e)
            {
                return false;
            }
        }

        public void Merge(DateRange dateRange)
        {
            if (dateRange.End > End)
            {
                End = dateRange.End;
            }

            if (dateRange.Start < Start)
            {
                Start = dateRange.Start;
            }
        }

        public void Coalesce(DateRange dateRange)
        {
            if (IsAdjacentToEnd(dateRange))
            {
                dateRange._afterEnd = this;
                _beforeStart = dateRange;
            }
            else if (IsAdjacentToStart(dateRange))
            {
                dateRange._beforeStart = this;
                _afterEnd = dateRange;
            }
            else
            {
                throw new ArgumentException("The specified date range is not adjacent.", nameof(dateRange));
            }
        }

        private bool IsAdjacentToEnd(DateRange dateRange)
        {
            var isAdjacent = Start == dateRange.End;

            return isAdjacent;
        }

        private bool IsAdjacentToStart(DateRange dateRange)
        {
            var isAdjacent = End == dateRange.Start;

            return isAdjacent;
        }

        public void MoveToStart(DateTime newStart)
        {
            var timeSpan = newStart - Start;
            
            Move(timeSpan);
        }

        private void MoveLeft(TimeSpan timeSpan)
        {
            if (BeforeStart != null)
            {
                BeforeStart.MoveThis(timeSpan);
                BeforeStart.MoveLeft(timeSpan);
            }
        }

        private void MoveRight(TimeSpan timeSpan)
        {
            if (AfterEnd != null)
            {
                AfterEnd.MoveThis(timeSpan);
                AfterEnd.MoveRight(timeSpan);
            }
        }

        private void MoveThis(TimeSpan timeSpan)
        {
            Start = Start.Add(timeSpan);   
            End = End.Add(timeSpan);
        }

        public void Move(TimeSpan timeSpan)
        {
            MoveThis(timeSpan);
            
            MoveLeft(timeSpan);
            MoveRight(timeSpan);
        }

        public void Extend(TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.Zero)
            {
                throw new ArgumentException("Cannot extend a date range by a negative amount", nameof(timeSpan));
            }
            
            End = End.Add(timeSpan);

            MoveRight(timeSpan);
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