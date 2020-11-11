using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamSeasonModel : BaseModel
    {
        public Guid ResultSetId { get; set; }
        
        public int CalendarYear { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public bool Default { get; set; }

        public virtual ResultSetModel ResultSet { get; set; }
    }
}