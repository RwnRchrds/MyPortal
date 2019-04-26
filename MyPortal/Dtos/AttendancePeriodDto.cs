using System;

namespace MyPortal.Dtos
{
    public class AttendancePeriodDto
    {
        public int Id { get; set; }

        public string Weekday { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}