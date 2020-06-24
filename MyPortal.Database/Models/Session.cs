using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Session")]
    public class Session
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid ClassId { get; set; }

        [Column(Order = 2)]
        public Guid PeriodId { get; set; }

        [Column(Order = 3)]
        public Guid TeacherId { get; set; }

        [Column(Order = 4)] 
        public Guid? RoomId { get; set; }

        public virtual StaffMember Teacher { get; set; }
        
        public virtual Period Period { get; set; }

        public virtual Class Class { get; set; }

        public virtual Room Room { get; set; }
    }
}
