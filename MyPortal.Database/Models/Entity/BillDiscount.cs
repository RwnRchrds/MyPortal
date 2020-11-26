using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("BillDiscounts")]
    public class BillDiscount : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid BillId { get; set; }

        [Column(Order = 2)]
        public Guid DiscountId { get; set; }

        [Column(Order = 3, TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Column(Order = 4)]
        public bool Percentage { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Discount Discount { get; set; }
    }
}
