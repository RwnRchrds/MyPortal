using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("AddressPerson")]
    public class AddressPerson
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid AddressId { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}