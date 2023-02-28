using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AddressTypes")]
    public class AddressType : BaseTypes.LookupItem
    {
        public AddressType()
        {
            AddressPersons = new HashSet<AddressPerson>();
        }
        
        public virtual ICollection<AddressPerson> AddressPersons { get; set; }
        public virtual ICollection<AddressAgency> AddressAgencies { get; set; }
    }
}