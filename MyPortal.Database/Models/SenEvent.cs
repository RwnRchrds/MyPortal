using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("SenEvent")]
    public class SenEvent
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public Guid EventTypeId { get; set; }

        [Column(Order = 3, TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(Order = 4)]
        [Required]
        public string Note { get; set; }

        public virtual Student Student { get; set; }

        public virtual SenEventType Type { get; set; }
    }
}