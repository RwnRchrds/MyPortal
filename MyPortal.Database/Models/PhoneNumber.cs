using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("PhoneNumber")]
    public class PhoneNumber
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid TypeId { get; set; }
        
        [Column(Order = 2)]
        public Guid PersonId { get; set; }

        [Column(Order = 3)]
        [Phone]
        [StringLength(128)]
        public string Number { get; set; }  

        public virtual PhoneNumberType Type { get; set; }
        public virtual Person Person { get; set; }
    }
}