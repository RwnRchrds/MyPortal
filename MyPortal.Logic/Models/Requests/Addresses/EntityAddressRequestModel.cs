﻿using System;

namespace MyPortal.Logic.Models.Requests.Addresses;

public class EntityAddressRequestModel : AddressRequestModel
{
    public Guid AddressTypeId { get; set; }
    public bool Main { get; set; }
}