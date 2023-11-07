using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamRooms")]
    public class ExamRoom : BaseTypes.Entity
    {
        public ExamRoom()
        {
            SeatBlocks = new HashSet<ExamRoomSeatBlock>();
        }

        [Column(Order = 2)] public Guid RoomId { get; set; }

        [Column(Order = 3)] public int Columns { get; set; }

        [Column(Order = 4)] public int Rows { get; set; }

        public virtual Room Room { get; set; }
        public virtual ICollection<ExamRoomSeatBlock> SeatBlocks { get; set; }
        public virtual ICollection<ExamComponentSitting> ExamComponentSittings { get; set; }
    }
}