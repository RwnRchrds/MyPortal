using System;

namespace MyPortal.Logic.Models.Requests.Addresses;

public class LinkAddressRequestModel
{
    public Guid AddressId { get; set; }
    public Guid EntityId { get; set; }
    public Guid AddressTypeId { get; set; }
    public bool Main { get; set; }
}