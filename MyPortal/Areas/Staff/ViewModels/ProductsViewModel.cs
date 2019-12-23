using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<ProductTypeDto> ProductTypes { get; set; }
        public ProductDto Product { get; set; }
    }
}