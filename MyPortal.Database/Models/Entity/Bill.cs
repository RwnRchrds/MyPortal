using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Bills")]
    public class Bill : BaseTypes.Entity
    {
        public Bill()
        {
            BillChargeDiscounts = new HashSet<BillDiscount>();
            BillStudentCharges = new HashSet<BillStudentCharge>();
            BillItems = new HashSet<BillItem>();
            AccountTransactions = new HashSet<BillAccountTransaction>();
        }

        [Column(Order = 2)] public Guid StudentId { get; set; }

        [Column(Order = 3)] public Guid? ChargeBillingPeriodId { get; set; }

        [Column(Order = 4)] public DateTime CreatedDate { get; set; }

        [Column(Order = 5)] public DateTime DueDate { get; set; }

        [Column(Order = 6)] public bool? Dispatched { get; set; }

        public virtual Student Student { get; set; }

        public virtual ChargeBillingPeriod ChargeBillingPeriod { get; set; }
        public virtual ICollection<BillDiscount> BillChargeDiscounts { get; set; }
        public virtual ICollection<BillStudentCharge> BillStudentCharges { get; set; }
        public virtual ICollection<BillItem> BillItems { get; set; }
        public virtual ICollection<BillAccountTransaction> AccountTransactions { get; set; }
    }
}