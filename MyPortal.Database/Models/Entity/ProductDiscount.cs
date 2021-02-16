using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("ProductDiscounts")]
    public class ProductDiscount : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ProductId { get; set; }
        
        [Column(Order = 2)]
        public Guid StoreDiscountId { get; set; }

        public virtual Product Product { get; set; }
        public virtual StoreDiscount StoreDiscount { get; set; }
    }
}
