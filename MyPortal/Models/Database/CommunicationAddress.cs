using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("Communication_Addresses")]
    public class CommunicationAddress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CommunicationAddress()
        {
            People = new HashSet<CommunicationAddressPerson>();
        }

        public int Id { get; set; }

        public string HouseNumber { get; set; }

        public string HouseName { get; set; }

        public string Apartment { get; set; }

        public string Street { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public bool Validated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommunicationAddressPerson> People { get; set; }
    }
}