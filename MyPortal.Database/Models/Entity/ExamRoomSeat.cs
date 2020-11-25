using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamRoomSeats")]
    public class ExamRoomSeat : BaseTypes.Entity
    {
        [Column(Order = 1)] 
        public Guid ExamRoomId { get; set; }

        [Column(Order = 2)]
        public int Column { get; set; }

        [Column(Order = 2)]
        public int Row { get; set; }

        [Column(Order = 3)]
        public bool DoNotUse { get; set; }

        public virtual ExamRoom ExamRoom { get; set; }
        public virtual ICollection<ExamSeatAllocation> SeatAllocations { get; set; }
    }
}
