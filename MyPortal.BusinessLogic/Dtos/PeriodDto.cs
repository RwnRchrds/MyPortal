using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Models.Attributes;

namespace MyPortal.BusinessLogic.Dtos
{
    public class PeriodDto
    {
        public int Id { get; set; }

        public DayOfWeek Weekday { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        [PeriodEndTime]
        public TimeSpan EndTime { get; set; }

        public bool IsAm { get; set; }

        public bool IsPm { get; set; }

        public string GetTimeDisplay()
        {
            return $"{StartTime.ToString(@"hh\:mm")} - {EndTime.ToString(@"hh\:mm")}";
        }
    }
}
