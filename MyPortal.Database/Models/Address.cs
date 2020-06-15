using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Address")]
    public class Address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            People = new HashSet<AddressPerson>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        [StringLength(128)]
        public string HouseNumber { get; set; }

        [DataMember]
        [StringLength(128)]
        public string HouseName { get; set; }

        [DataMember]
        [StringLength(128)]
        public string Apartment { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Street { get; set; }

        [DataMember]
        [StringLength(256)]
        public string District { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string Town { get; set; }

        [DataMember]
        [Required]
        [StringLength(256)]
        public string County { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Postcode { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Country { get; set; }

        [DataMember]
        public bool Validated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddressPerson> People { get; set; }
    }
}