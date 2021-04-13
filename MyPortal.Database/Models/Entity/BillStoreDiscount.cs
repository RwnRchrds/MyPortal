using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    public class BillStoreDiscount : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid BillId { get; set; }
        
        [Column(Order = 2)]
        public Guid StoreDiscountId { get; set; }
        
        [Column(Order = 3, TypeName = "decimal(10,2)")]
        public decimal GrossAmount { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual StoreDiscount StoreDiscount { get; set; }
    }
}