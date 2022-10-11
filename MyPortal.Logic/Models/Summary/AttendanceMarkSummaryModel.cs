
using System;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Summary
{    
    public class AttendanceMarkSummaryModel
    {
        public Guid StudentId { get; set; }
        
        public Guid WeekId { get; set; }

        public Guid PeriodId { get; set; }

        [NotNull]
        public Guid? CodeId { get; set; }

        public string Comments { get; set; }

        public int? MinutesLate { get; set; }
    }
}