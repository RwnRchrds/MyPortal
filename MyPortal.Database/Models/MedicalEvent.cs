using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("MedicalEvent")]
    public class MedicalEvent : IEntity
    {
        [Column(Order = 1)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 2)]
        public Guid StudentId { get; set; }

        [Column(Order = 3)]
        public Guid RecordedById { get; set; }

        [Column(Order = 4, TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(Order = 5)]
        [Required]
        public string Note { get; set; }

        public virtual ApplicationUser RecordedBy { get; set; } 

        public virtual Student Student { get; set; }
    }
}