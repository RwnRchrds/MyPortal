﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("VatRates")]
    public class VatRate : LookupItem
    {
        public VatRate()
        {
            Products = new HashSet<Product>();
        }

        [Column(Order = 4, TypeName = "decimal(10,2)")]
        public decimal Value { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}