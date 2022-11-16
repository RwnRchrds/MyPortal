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
            AddressPeople = new HashSet<AddressPerson>();
            AddressAgencies = new HashSet<AddressAgency>();
        }

        [Column(Order = 1)]
        [StringLength(128)]
        public string BuildingNumber { get; set; }

        [Column(Order = 2)]
        [StringLength(128)]
        public string BuildingName { get; set; }

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
        
        public virtual ICollection<AddressAgency> AddressAgencies { get; set; }
        public virtual ICollection<AddressPerson> AddressPeople { get; set; }
    }
}