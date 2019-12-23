using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class SaleEntryViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public SaleDto Sale { get; set; }
    }
}