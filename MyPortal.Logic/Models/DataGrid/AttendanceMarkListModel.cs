
using System;

namespace MyPortal.Logic.Models.DataGrid
{    
    public class AttendanceMarkListModel
    {        
        public Guid Id { get; set; }
        
        public Guid StudentId { get; set; }
        
        public Guid WeekId { get; set; }

        public Guid PeriodId { get; set; }
        
        public string Mark { get; set; }

        public string Comments { get; set; }

        public int? MinutesLate { get; set; }
    }
}