using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("ProductTypeDiscounts")]
    public class ProductTypeDiscount : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid StoreDiscountId { get; set; }

        [Column(Order = 2)]
        public Guid ProductTypeId { get; set; }

        public virtual StoreDiscount StoreDiscount { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
