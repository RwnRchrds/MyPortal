
using System;

namespace MyPortal.Logic.Models.List
{    
    public class AttendanceMarkListModel
    {
        public Guid StudentId { get; set; }
        
        public Guid WeekId { get; set; }

        public Guid PeriodId { get; set; }

        public Guid CodeId { get; set; }

        public string Comments { get; set; }

        public int? MinutesLate { get; set; }
    }
}