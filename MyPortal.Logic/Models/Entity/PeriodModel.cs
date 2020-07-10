using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Entity
{
    public class PeriodModel
    {
        public Guid Id { get; set; }

        public Guid WeekPatternId { get; set; }

        public DayOfWeek Weekday { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool AmReg { get; set; }

        public bool PmReg { get; set; }

        public virtual AttendanceWeekPattern WeekPattern { get; set; }
    }
}
