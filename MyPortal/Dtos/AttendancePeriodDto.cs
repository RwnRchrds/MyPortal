using System;

namespace MyPortal.Dtos
{
    public class AttendancePeriodDto
    {
        public int Id { get; set; }

        public string Weekday { get; set; }

        public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsAm { get; set; }

        public bool IsPm { get; set; }
    }
}