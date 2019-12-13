using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string HouseNumber { get; set; }
        public string HouseName { get; set; }
        public string Apartment { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public bool Validated { get; set; }
    }
}
