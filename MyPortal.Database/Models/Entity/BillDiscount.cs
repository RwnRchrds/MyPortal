﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("BillDiscounts")]
    public class BillDiscount : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid BillId { get; set; }

        [Column(Order = 3)] public Guid DiscountId { get; set; }

        [Column(Order = 4, TypeName = "decimal(10,2)")]
        public decimal GrossAmount { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Discount Discount { get; set; }
    }
}