using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Sale")]
    public class Sale
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        [Column(TypeName = "decimal(10,2)")]
        public decimal AmountPaid { get; set; }
        
        [DataMember]
        public bool Processed { get; set; }

        [DataMember]
        public bool Refunded { get; set; }

        [DataMember]
        public bool Deleted { get; set; }

        public virtual Student Student { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Product Product { get; set; }
    }
}
