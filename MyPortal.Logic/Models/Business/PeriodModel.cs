using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Business
{
    public class PeriodModel
    {
        public Guid Id { get; set; }

        public DayOfWeek Weekday { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsAm { get; set; }

        public bool IsPm { get; set; }
    }
}
