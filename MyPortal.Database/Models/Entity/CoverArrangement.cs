using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("CoverArrangements")]
    public class CoverArrangement : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid WeekId { get; set; }

        [Column(Order = 2)]
        public Guid SessionId { get; set; }

        [Column(Order = 3)]
        public Guid? TeacherId { get; set; }

        [Column(Order = 4)]
        public Guid? RoomId { get; set; }

        [Column(Order = 5)]
        public string Comments { get; set; }

        public virtual AttendanceWeek Week { get; set; }
        public virtual Session Session { get; set; }
        public virtual StaffMember Teacher { get; set; }
        public virtual Room Room { get; set; }
    }
}
