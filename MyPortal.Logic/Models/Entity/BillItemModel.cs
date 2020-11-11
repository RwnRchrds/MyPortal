using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class BillItemModel : BaseModel
    {
        public Guid BillId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public bool CustomerReceived { get; set; }

        public virtual BillModel Bill { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}
