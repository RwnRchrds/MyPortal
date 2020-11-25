using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("BillAccountTransactions")]
    public class BillAccountTransaction : BaseTypes.Entity
    {
        public Guid BillId { get; set; }
        public Guid AccountTransactionId { get; set; }

        public virtual Bill Bill { get; set; }
        public virtual AccountTransaction AccountTransaction { get; set; }
    }
}
