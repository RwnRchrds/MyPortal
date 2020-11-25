using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("RoomClosures")]
    public class RoomClosure : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid RoomId { get; set; }

        [Column(Order = 2)]
        public Guid ReasonId { get; set; }

        [Column(Order = 3)]
        public DateTime StartDate { get; set; }

        [Column(Order = 4)]
        public DateTime EndDate { get; set; }

        [Column(Order = 5)]
        [StringLength(256)]
        public string Notes { get; set; }

        public virtual Room Room { get; set; }
        public virtual RoomClosureReason Reason { get; set; }
    }
}
