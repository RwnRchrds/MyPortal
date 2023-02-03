using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("MedicalEvents")]
    public class MedicalEvent : BaseTypes.Entity, ICreatable
    {
        [Column(Order = 1)]
        public Guid PersonId { get; set; }

        [Column(Order = 2)]
        public Guid CreatedById { get; set; }
        
        [Column(Order = 3)] 
        public DateTime CreatedDate { get; set; }

        [Column(Order = 4, TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(Order = 5)]
        [Required]
        public string Note { get; set; }

        public virtual User CreatedBy { get; set; } 

        public virtual Person Person { get; set; }
    }
}