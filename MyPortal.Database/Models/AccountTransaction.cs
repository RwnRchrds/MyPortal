using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("AccountTransactions")]
    public class AccountTransaction : Entity
    {
        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2, TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public bool Credit { get; set; }

        public DateTime Date { get; set; } 

        public virtual Student Student { get; set; }
    }
}
