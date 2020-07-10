using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("AddressPerson")]
    public class AddressPerson : IEntity
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid AddressId { get; set; }

        [Column(Order = 2)]
        public Guid PersonId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}