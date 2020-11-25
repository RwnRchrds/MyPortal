using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Bills")]
    public class Bill : BaseTypes.Entity, ISoftDeleteEntity
    {
        public Bill()
        {
            BillItems = new HashSet<BillItem>();
        }
        
        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public DateTime Date { get; set; }

        [Column(Order = 3, TypeName = "decimal(10,2)")]
        public decimal NetAmount { get; set; }
        
        [Column(Order = 4, TypeName = "decimal(10,2)")]
        public decimal TaxAmount { get; set; }
        
        [Column(Order = 5, TypeName = "decimal(10,2)")]
        public decimal AmountPaid { get; set; }

        [Column(Order = 6)]
        public BillStatus Status { get; set; }

        [Column(Order = 7)]
        public bool Deleted { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<BillItem> BillItems { get; set; }
        public virtual ICollection<BillAccountTransaction> AccountTransactions { get; set; }
    }
}
