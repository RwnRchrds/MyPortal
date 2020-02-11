using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class AttendanceMarkDto
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid WeekId { get; set; }

        public Guid PeriodId { get; set; }

        [Required]
        [StringLength(1)]
        public string Mark { get; set; }

        [StringLength(256)]
        public string Comments { get; set; }

        public int MinutesLate { get; set; }

        public virtual PeriodDto Period { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual AttendanceWeekDto Week { get; set; }
    }
}
