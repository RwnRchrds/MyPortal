using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("BillCharges")]
    public class BillCharge : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid BillId { get; set; }

        [Column(Order = 2)]
        public Guid ChargeId { get; set; }

        [Column(Order = 3, TypeName = "decimal(10,2)")]
        public decimal GrossAmount { get; set; }

        [Column(Order = 4)]
        public bool Refunded { get; set; } 

        public virtual Bill Bill { get; set; }
        public virtual Charge Charge { get; set; }
    }
}
