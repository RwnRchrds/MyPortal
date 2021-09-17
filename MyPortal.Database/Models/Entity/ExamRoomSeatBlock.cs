using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamRoomSeatBlocks")]
    public class ExamRoomSeatBlock : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ExamRoomId { get; set; }
        
        [Column(Order = 2)]
        public int SeatRow { get; set; }
        
        [Column(Order = 3)]
        public int SeatColumn { get; set; }
        
        [Column(Order = 4)]
        public string Comments { get; set; }

        public virtual ExamRoom ExamRoom { get; set; }
    }
}