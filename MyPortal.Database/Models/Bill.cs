using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Constants;

namespace MyPortal.Database.Models
{
    [Table("Bills")]
    public class Bill : Entity
    {
        public Bill()
        {
            BillItems = new HashSet<BillItem>();
        }
        
        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 4)]
        public DateTime Date { get; set; }

        [Column(Order = 5, TypeName = "decimal(10,2)")]
        public decimal NetAmount { get; set; }
        
        [Column(Order = 6, TypeName = "decimal(10,2)")]
        public decimal TaxAmount { get; set; }
        
        [Column(Order = 7, TypeName = "decimal(10,2)")]
        public decimal AmountPaid { get; set; }

        [Column(Order = 8)]
        public BillStatus Status { get; set; }

        [Column(Order = 9)]
        public bool Deleted { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<BillItem> BillItems { get; set; }
    }
}
