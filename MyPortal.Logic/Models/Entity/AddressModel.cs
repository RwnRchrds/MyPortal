using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AddressModel : BaseModel
    {
        public AddressModel(Address model) : base(model)
        {
            BuildingNumber = model.BuildingNumber;
            BuildingName = model.BuildingName;
            Apartment = model.Apartment;
            Street = model.Street;
            District = model.District;
            Town = model.Town;
            County = model.County;
            Postcode = model.Postcode;
            Country = model.Country;
            Validated = model.Validated;
        }
        
        [StringLength(128)]
        public string BuildingNumber { get; set; }

        [StringLength(128)]
        public string BuildingName { get; set; }

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
