using MyPortal.Models.Database;

namespace MyPortal.Dtos.Lite
{    
    public class AttendanceMarkLiteDto
    {        
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        
        public int WeekId { get; set; }

        public int PeriodId { get; set; }
        
        public string Mark { get; set; }
        
        public AttendanceMeaning Meaning { get; set; }
    }
}