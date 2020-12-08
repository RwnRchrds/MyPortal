using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("BillItems")]
    public class BillItem : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid BillId { get; set; }

        [Column(Order = 2)]
        public Guid ProductId { get; set; }

        [Column(Order = 3)]
        public int Quantity { get; set; }

        [Column(Order = 4)]
        public decimal GrossAmount { get; set; }

        [Column(Order = 4)]
        public bool CustomerReceived { get; set; }

        [Column(Order = 5)]
        public bool Refunded { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}