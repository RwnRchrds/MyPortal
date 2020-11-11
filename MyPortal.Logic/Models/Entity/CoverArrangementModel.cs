using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class CoverArrangementModel : BaseModel
    {
        public Guid WeekId { get; set; }
        
        public Guid SessionId { get; set; }
        
        public Guid? TeacherId { get; set; }
        
        public Guid? RoomId { get; set; }
        
        public string Comments { get; set; }

        public virtual AttendanceWeekModel Week { get; set; }
        public virtual SessionModel Session { get; set; }
        public virtual StaffMemberModel Teacher { get; set; }
        public virtual RoomModel Room { get; set; }

        public bool StaffChanged => TeacherId != null;

        public bool RoomChanged => RoomId != null;
    }
}