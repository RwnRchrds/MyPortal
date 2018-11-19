using System.Collections.Generic;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class SaleEntryViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Sale Sale { get; set; }
    }
}