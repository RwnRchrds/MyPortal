using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Search
{
    public class AddressSearchOptions
    {
        public string Postcode { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
    }
}
