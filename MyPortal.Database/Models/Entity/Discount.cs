using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("Discounts")]
    public class Discount : LookupItem
    {
        [Column(Order = 3)]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 4, TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Column(Order = 5)]
        public bool Percentage { get; set; }
        
        [Column(Order = 6)] 
        public int MaxUsage { get; set; }

        public virtual ICollection<ChargeDiscount> ChargeDiscounts { get; set; }
        public virtual ICollection<StoreDiscount> StoreDiscounts { get; set; }
        public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; }
    }
}
