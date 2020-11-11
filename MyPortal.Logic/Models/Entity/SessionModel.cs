using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SessionModel : BaseModel
    {
        public Guid ClassId { get; set; }
        
        public Guid PeriodId { get; set; }
        
        public Guid TeacherId { get; set; }
        
        public Guid? RoomId { get; set; }

        public virtual StaffMemberModel Teacher { get; set; }
        
        public virtual AttendancePeriodModel AttendancePeriod { get; set; }

        public virtual ClassModel Class { get; set; }

        public virtual RoomModel Room { get; set; }
    }
}