using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("ChargeBillingPeriods")]
public class ChargeBillingPeriod : BaseTypes.Entity
{
    [Column(Order = 1)]
    public string Name { get; set; }
    
    [Column(Order = 2)]
    public DateTime StartDate { get; set; }
    
    [Column(Order = 3)]
    public DateTime EndDate { get; set; }

    public virtual ICollection<Bill> Bills { get; set; }
    public virtual ICollection<StudentCharge> StudentCharges { get; set; }
}