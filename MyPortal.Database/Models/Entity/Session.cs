using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Sessions")]
    public class Session : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ClassId { get; set; }

        [Column(Order = 2)]
        public Guid PeriodId { get; set; }

        [Column(Order = 3)]
        public Guid TeacherId { get; set; }

        [Column(Order = 4)] 
        public Guid? RoomId { get; set; }

        public virtual StaffMember Teacher { get; set; }
        
        public virtual AttendancePeriod AttendancePeriod { get; set; }

        public virtual Class Class { get; set; }

        public virtual Room Room { get; set; }
    }
}
