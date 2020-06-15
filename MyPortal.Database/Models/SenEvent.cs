using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("SenEvent")]
    public class SenEvent
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid EventTypeId { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [DataMember]
        [Required]
        public string Note { get; set; }

        public virtual Student Student { get; set; }

        public virtual SenEventType Type { get; set; }
    }
}