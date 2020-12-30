using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Addresses")]
    public class Address : BaseTypes.Entity
    {
        public Address()
        {
            People = new HashSet<AddressPerson>();
        }

        [Column(Order = 1)]
        [StringLength(128)]
        public string HouseNumber { get; set; }

        [Column(Order = 2)]
        [StringLength(128)]
        public string HouseName { get; set; }

        [Column(Order = 3)]
        [StringLength(128)]
        public string Apartment { get; set; }

        [Column(Order = 4)]
        [Required]
        [StringLength(256)]
        public string Street { get; set; }

        [Column(Order = 5)]
        [StringLength(256)]
        public string District { get; set; }

        [Column(Order = 6)]
        [Required]
        [StringLength(256)]
        public string Town { get; set; }

        [Column(Order = 7)]
        [Required]
        [StringLength(256)]
        public string County { get; set; }

        [Column(Order = 8)]
        [Required]
        [StringLength(128)]
        public string Postcode { get; set; }

        [Column(Order = 9)]
        [Required]
        [StringLength(128)]
        public string Country { get; set; }

        [Column(Order = 10)]
        public bool Validated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddressPerson> People { get; set; }

        public virtual ICollection<Agency> Agencies { get; set; }
    }
}