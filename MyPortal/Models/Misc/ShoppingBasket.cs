using System.Collections.Generic;

namespace MyPortal.Models.Misc
{
    public class ShoppingBasket
    {
        public int Student { get; set; }
        public IList<Product> Products { get; set; }
    }
}