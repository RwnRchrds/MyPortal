using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Dtos;
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