using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [StringLength(256)]
        public string Street { get; set; }

        [StringLength(256)]
        public string District { get; set; }

        [Required]
        [StringLength(256)]
        public string Town { get; set; }

        [Required]
        [StringLength(256)]
        public string County { get; set; }

        [Required]
        [StringLength(128)]
        public string Postcode { get; set; }

        [Required]
        [StringLength(128)]
        public string Country { get; set; }

        public bool Validated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommunicationAddressPerson> People { get; set; }
    }
}