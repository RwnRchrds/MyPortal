using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("StoreDiscounts")]
    public class StoreDiscount : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid DiscountId { get; set; }

        [Column(Order = 2)]
        [Range(0, int.MaxValue)]
        public int MinQuantity { get; set; }

        [Column(Order = 3)]
        [Range(0, int.MaxValue)]
        public int? MaxQuantity { get; set; }

        [Column(Order = 5)]
        public bool Auto { get; set; }

        public virtual Discount Discount { get; set; }
        public virtual ICollection<BillStoreDiscount> BillStoreDiscounts { get; set; }
        public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; }
        public virtual ICollection<ProductTypeDiscount> ProductTypeDiscounts { get; set; }
    }
}
