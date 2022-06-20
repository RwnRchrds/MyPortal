using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("StoreDiscounts")]
    public class StoreDiscount : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid? ProductId { get; set; }
        
        [Column(Order = 2)]
        public Guid? ProductTypeId { get; set; }
        
        [Column(Order = 3)]
        public Guid DiscountId { get; set; }

        // Apply the discount to the cart total
        public bool ApplyToCart { get; set; }
        
        // How many of this product in the basket should the discount apply to
        // E.g. If the user has 3 apples in their basket but you only want to apply discount to 1 of them
        // For example, in a BOGOF situaton where you apply 100% discount to one of the 2 items
        // null for all
        [Column(Order = 4)] 
        public int? ApplyTo { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Discount Discount { get; set; }
    }
}
