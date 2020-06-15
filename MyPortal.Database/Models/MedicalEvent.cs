using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("MedicalEvent")]
    public class MedicalEvent
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid RecordedById { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [DataMember]
        [Required]
        public string Note { get; set; }

        public virtual ApplicationUser RecordedBy { get; set; } 

        public virtual Student Student { get; set; }
    }
}