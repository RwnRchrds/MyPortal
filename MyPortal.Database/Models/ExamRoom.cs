using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("ExamRooms")]
    public class ExamRoom : Entity
    {
        [Column(Order = 1)]
        public Guid RoomId { get; set; }

        [Column(Order = 2)]
        public int Columns { get; set; }

        [Column(Order = 3)]
        public int Rows { get; set; }

        public virtual Room Room { get; set; }
    }
}
