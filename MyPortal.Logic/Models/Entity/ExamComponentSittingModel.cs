using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamComponentSittingModel : BaseModel
    {
        public Guid ComponentId { get; set; }
        
        public Guid ExamRoomId { get; set; }
        
        public DateTime ExamDate { get; set; }
        
        public TimeSpan? ActualStartTime { get; set; }
        
        public int ExtraTimePercent { get; set; }

        public virtual ExamComponentModel Component { get; set; }
        public virtual ExamRoomModel Room { get; set; }
    }
}