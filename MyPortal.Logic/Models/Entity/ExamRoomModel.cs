using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamRoomModel : BaseModel
    {
        public Guid RoomId { get; set; }
        
        public int Columns { get; set; }
        
        public int Rows { get; set; }

        public virtual RoomModel Room { get; set; }
    }
}