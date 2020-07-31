using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("AddressPeople")]
    public class AddressPerson : Entity
    {
        [Column(Order = 1)]
        public Guid AddressId { get; set; }

        [Column(Order = 2)]
        public Guid PersonId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}