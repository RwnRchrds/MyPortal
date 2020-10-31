using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Entity
{
    public class BillItemModel
    {
        public Guid BillId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public bool CustomerReceived { get; set; }

        public virtual BillModel Bill { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}
