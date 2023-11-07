using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("SenEvents")]
    public class SenEvent : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid StudentId { get; set; }

        [Column(Order = 3)] public Guid EventTypeId { get; set; }

        [Column(Order = 4, TypeName = "date")] public DateTime Date { get; set; }

        [Column(Order = 5)] [Required] public string Note { get; set; }

        public virtual Student Student { get; set; }

        public virtual SenEventType Type { get; set; }
    }
}