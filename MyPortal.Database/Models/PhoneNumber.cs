using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("PhoneNumber")]
    public class PhoneNumber
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid TypeId { get; set; }
        
        [DataMember]
        public Guid PersonId { get; set; }

        [DataMember]
        [Phone]
        [StringLength(128)]
        public string Number { get; set; }  

        public virtual PhoneNumberType Type { get; set; }
        public virtual Person Person { get; set; }
    }
}