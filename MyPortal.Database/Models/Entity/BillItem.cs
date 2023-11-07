using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("BillItems")]
    public class BillItem : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid BillId { get; set; }

        [Column(Order = 3)] public Guid ProductId { get; set; }

        [Column(Order = 4)] public int Quantity { get; set; }

        [Column(Order = 5, TypeName = "decimal(10,2)")]
        public decimal NetAmount { get; set; }

        [Column(Order = 6, TypeName = "decimal(10,2)")]
        public decimal VatAmount { get; set; }

        [Column(Order = 7)] public bool CustomerReceived { get; set; }

        [Column(Order = 8)] public bool Refunded { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual Product Product { get; set; }
    }
}