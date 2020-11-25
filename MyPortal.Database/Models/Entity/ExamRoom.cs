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
            Seats = new HashSet<ExamRoomSeat>();
        }

        [Column(Order = 1)]
        public Guid RoomId { get; set; }

        [Column(Order = 2)]
        public int Columns { get; set; }

        [Column(Order = 3)]
        public int Rows { get; set; }

        public virtual Room Room { get; set; }
        public virtual ICollection<ExamRoomSeat> Seats { get; set; }
        public virtual ICollection<ExamComponentSitting> ExamComponentSittings { get; set; }
    }
}
