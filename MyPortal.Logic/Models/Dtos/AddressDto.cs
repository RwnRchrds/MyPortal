using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string HouseNumber { get; set; }

        [StringLength(128)]
        public string HouseName { get; set; }

        [StringLength(128)]
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
    }
}
