using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamRoomSeatModel : BaseModel
    {
        public Guid ExamRoomId { get; set; }
        
        public int Column { get; set; }
        
        public int Row { get; set; }
        
        public bool DoNotUse { get; set; }

        public virtual ExamRoomModel ExamRoom { get; set; }
    }
}