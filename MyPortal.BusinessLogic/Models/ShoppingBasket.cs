using System.Collections.Generic;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Models
{
    public class ShoppingBasket
    {
        public int StudentId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}