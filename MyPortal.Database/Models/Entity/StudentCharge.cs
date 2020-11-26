using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentCharges")]
    public class StudentCharge : BaseTypes.Entity
    {
        public StudentCharge()
        {
            BillCharges = new HashSet<BillCharge>();
        }

        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public Guid ChargeId { get; set; }

        [Column(Order = 3)] 
        public DateTime CreatedDate { get; set; }

        [Column(Order = 3, TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Column(Order = 4)]
        public int Recurrences { get; set; }

        public virtual Student Student { get; set; }
        public virtual Charge Charge { get; set; }

        public virtual ICollection<BillCharge> BillCharges { get; set; }
    }
}
