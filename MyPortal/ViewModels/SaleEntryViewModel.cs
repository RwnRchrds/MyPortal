using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class SaleEntryViewModel
    {
        public IEnumerable<FinanceProduct> Products { get; set; }
        public FinanceSale Sale { get; set; }
    }
}