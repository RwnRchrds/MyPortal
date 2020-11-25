using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AccountTransactions")]
    public class AccountTransaction : BaseTypes.Entity
    {
        public AccountTransaction()
        {
            BillAccountTransactions = new HashSet<BillAccountTransaction>();
        }

        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2, TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public bool Credit { get; set; }

        public DateTime Date { get; set; } 

        public virtual Student Student { get; set; }

        public virtual ICollection<BillAccountTransaction> BillAccountTransactions { get; set; }    
    }
}
    