using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Dtos
{
    public class PeriodDto
    {
        public int Id { get; set; }

        public DayOfWeek Weekday { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsAm { get; set; }

        public bool IsPm { get; set; }

        public string GetTimeDisplay()
        {
            return $"{StartTime.ToString(@"hh\:mm")} - {EndTime.ToString(@"hh\:mm")}";
        }
    }
}
