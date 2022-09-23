using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AddressTypes")]
    public class AddressType : BaseTypes.LookupItem
    {
        public AddressType()
        {
            AddressPersons = new HashSet<AddressLink>();
        }
        
        public virtual ICollection<AddressLink> AddressPersons { get; set; }
    }
}