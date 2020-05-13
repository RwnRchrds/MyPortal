
namespace MyPortal.Logic.Models.Summary
{    
    public class AttendanceMarkListModel
    {        
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        
        public int WeekId { get; set; }

        public int PeriodId { get; set; }
        
        public string Mark { get; set; }

        public string Comments { get; set; }

        public int? MinutesLate { get; set; }
    }
}