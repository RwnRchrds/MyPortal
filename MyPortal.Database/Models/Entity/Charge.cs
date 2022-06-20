using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("Charges")]
    public class Charge : LookupItem
    {
        public Charge()
        {
            StudentCharges = new HashSet<StudentCharge>();
        }

        [Column(Order = 3)]
        public Guid VatRateId { get; set; }

        [Column(Order = 4)]
        [StringLength(64)]
        public string Code { get; set; }

        [Column(Order = 5)]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 6, TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Column(Order = 7)] 
        public bool Variable { get; set; }

        public virtual VatRate VatRate { get; set; }
        public virtual ICollection<StudentCharge> StudentCharges { get; set; }
        public virtual ICollection<ChargeDiscount> ChargeDiscounts { get; set; }
    }
}
