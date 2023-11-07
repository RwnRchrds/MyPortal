using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentChargeDiscounts")]
    public class StudentChargeDiscount : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid StudentId { get; set; }

        [Column(Order = 3)] public Guid ChargeDiscountId { get; set; }

        public virtual Student Student { get; set; }
        public virtual ChargeDiscount ChargeDiscount { get; set; }
    }
}