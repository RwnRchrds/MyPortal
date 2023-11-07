using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("AddressAgencies")]
public class AddressAgency : BaseTypes.Entity
{
    [Column(Order = 2)] public Guid AddressId { get; set; }

    [Column(Order = 3)] public Guid AgencyId { get; set; }

    [Column(Order = 4)] public Guid AddressTypeId { get; set; }

    [Column(Order = 5)] public bool Main { get; set; }

    public virtual Address Address { get; set; }
    public virtual Agency Agency { get; set; }
    public virtual AddressType AddressType { get; set; }
}