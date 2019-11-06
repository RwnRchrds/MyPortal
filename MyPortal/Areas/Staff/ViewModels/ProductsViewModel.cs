using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<FinanceProductType> ProductTypes { get; set; } 
        public FinanceProduct Product { get; set; }
    }
}