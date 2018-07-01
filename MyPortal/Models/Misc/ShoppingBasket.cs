using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Misc
{
    public class ShoppingBasket
    {
        public int Student { get; set; }
        public IList<Product> Products { get; set; }
    }
}