
namespace MyPortal.Logic.Models.Lite
{    
    public class AttendanceMarkLiteDto
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