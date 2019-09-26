using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models.Database
{
    [Table("Communication_PhoneNumberTypes")]
    public class CommunicationPhoneNumberType
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommunicationPhoneNumber> PhoneNumbers { get; set; }
    }
}