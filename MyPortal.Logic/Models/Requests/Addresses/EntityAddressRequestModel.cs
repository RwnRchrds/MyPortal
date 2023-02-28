using System;

namespace MyPortal.Logic.Models.Requests.Addresses;

public class EntityAddressRequestModel : AddressRequestModel
{
    public Guid EntityId { get; set; }
    public Guid AddressTypeId { get; set; }
}