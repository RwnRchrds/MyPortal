using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models
{
    [Table("Cover")]
    public class Cover
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get; set; }

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
