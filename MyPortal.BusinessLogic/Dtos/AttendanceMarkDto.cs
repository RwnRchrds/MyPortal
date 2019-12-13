using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class AttendanceMarkDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int WeekId { get; set; }
        public int PeriodId { get; set; }
        public string Mark { get; set; }
        public string Comments { get; set; }
        public int MinutesLate { get; set; }
        public virtual PeriodDto Period { get; set; }
        public virtual StudentDto Student { get; set; }
        public virtual AttendanceWeekDto Week { get; set; }
    }
}
