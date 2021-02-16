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
        public Guid DiscountId { get; set; }

        [Column(Order = 3)]
        public int MinQuantity { get; set; }

        [Column(Order = 4)]
        public int? MaxQuantity { get; set; }

        [Column(Order = 2)]
        public bool Global { get; set; }

        public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; }
        public virtual ICollection<ProductTypeDiscount> ProductTypeDiscounts { get; set; }
    }
}
