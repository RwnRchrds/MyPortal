using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("Discounts")]
    public class Discount : LookupItem
    {
        [Column(Order = 4)]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 5, TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Column(Order = 6)] public bool Percentage { get; set; }

        // Specify whether this discount can be combined with other discounts
        [Column(Order = 7)] public bool BlockOtherDiscounts { get; set; }

        public virtual ICollection<BillDiscount> BillDiscounts { get; set; }
        public virtual ICollection<ChargeDiscount> ChargeDiscounts { get; set; }
        public virtual ICollection<StoreDiscount> ProductDiscounts { get; set; }
    }
}