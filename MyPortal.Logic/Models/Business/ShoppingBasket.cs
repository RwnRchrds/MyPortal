using System.Collections.Generic;

namespace MyPortal.Logic.Models.Business
{
    public class ShoppingBasket
    {
        public int StudentId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}