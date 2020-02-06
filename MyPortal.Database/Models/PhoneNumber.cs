using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("PhoneNumber")]
    public class PhoneNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public Guid PersonId { get; set; }

        [Phone]
        [StringLength(128)]
        public string Number { get; set; }  

        public virtual PhoneNumberType Type { get; set; }
        public virtual Person Person { get; set; }
    }
}