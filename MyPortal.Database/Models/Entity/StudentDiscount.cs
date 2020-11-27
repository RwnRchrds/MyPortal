using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentDiscounts")]
    public class StudentDiscount : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public Guid DiscountId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Discount Discount { get; set; }
    }
}
