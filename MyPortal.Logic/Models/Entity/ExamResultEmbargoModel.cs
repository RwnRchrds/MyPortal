using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamResultEmbargoModel : BaseModel
    {
        public Guid ResultSetId { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }

        public virtual ResultSetModel ResultSet { get; set; }
    }
}