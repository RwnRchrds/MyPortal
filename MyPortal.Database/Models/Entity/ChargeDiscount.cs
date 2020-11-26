using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("ChargeDiscounts")]
    public class ChargeDiscount : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ChargeId { get; set; }

        [Column(Order = 2)]
        public Guid DiscountId { get; set; }

        public virtual Charge Charge { get; set; }
        public virtual Discount Discount { get; set; }
    }
}
