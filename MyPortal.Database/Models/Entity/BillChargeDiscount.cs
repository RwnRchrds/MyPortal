using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("BillChargeDiscounts")]
    public class BillChargeDiscount : BaseTypes.Entity
    {
        [Column(Order = 1)] public Guid BillId { get; set; }

        [Column(Order = 2)] public Guid ChargeDiscountId { get; set; }

        [Column(Order = 3, TypeName = "decimal(10,2)")]
        public decimal GrossAmount { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual ChargeDiscount ChargeDiscount { get; set; }
    }
}
