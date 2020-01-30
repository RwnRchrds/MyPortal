using System.Collections.Generic;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Business
{
    public class ShoppingBasket
    {
        public int StudentId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}