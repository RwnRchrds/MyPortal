using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Bills")]
    public class Bill : BaseTypes.Entity
    {
        public Bill()
        {
            BillItems = new HashSet<BillItem>();
            AccountTransactions = new HashSet<BillAccountTransaction>();
        }
        
        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 3)] 
        public DateTime DueDate { get; set; }

        [Column(Order = 4)] 
        public bool? Dispatched { get; set; }

        public virtual Student Student { get; set; }

        public virtual ICollection<BillDiscount> BillDiscounts { get; set; }
        public virtual ICollection<BillCharge> BillCharges { get; set; }    
        public virtual ICollection<BillItem> BillItems { get; set; }
        public virtual ICollection<BillAccountTransaction> AccountTransactions { get; set; }
    }
}
