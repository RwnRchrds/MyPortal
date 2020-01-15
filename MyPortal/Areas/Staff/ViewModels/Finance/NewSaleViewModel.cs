using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Models.Data;

namespace MyPortal.Areas.Staff.ViewModels.Finance
{
    public class NewSaleViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public ShoppingBasket Basket { get; set; }
    }
}