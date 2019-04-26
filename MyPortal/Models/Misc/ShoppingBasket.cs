using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Models.Misc
{
    public class ShoppingBasket
    {
        public int StudentId { get; set; }
        public IEnumerable<FinanceProduct> Products { get; set; }
    }
}