using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("BillingCycles")]
    public class BillingCycle : LookupItem
    {
        [Column(Order = 3)]
        public int Days { get; set; }

        [Column(Order = 4)]
        public int Months { get; set; }
    }
}
