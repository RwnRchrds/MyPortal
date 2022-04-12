using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentCharges")]
    public class StudentCharge : BaseTypes.Entity
    {
        public StudentCharge()
        {
            
        }

        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public Guid ChargeId { get; set; }
        
        [Column(Order = 3)] 
        public string Description { get; set; }

        [Column(Order = 4)] 
        public DateTime StartDate { get; set; }

        [Column(Order = 5)]
        public int Recurrences { get; set; }

        public virtual Student Student { get; set; }
        public virtual Charge Charge { get; set; }
    }
}
