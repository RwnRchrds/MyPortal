using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AddressPeople")]
    public class AddressPerson : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid AddressId { get; set; }

        [Column(Order = 2)]
        public Guid PersonId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}