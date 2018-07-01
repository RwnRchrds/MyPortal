using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class NewSaleViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public ShoppingBasket Basket { get; set; }
    }
}