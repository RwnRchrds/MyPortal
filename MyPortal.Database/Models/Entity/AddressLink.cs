using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AddressLinks")]
    public class AddressLink : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid AddressId { get; set; }

        [Column(Order = 2)]
        public Guid? PersonId { get; set; }
        
        [Column(Order = 3)]
        public Guid? AgencyId { get; set; }
        
        [Column(Order = 3)] 
        public Guid AddressTypeId { get; set; }
        
        [Column(Order = 4)] 
        public bool Main { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual AddressType AddressType { get; set; }
    }
}