﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Contacts;


namespace MyPortal.Logic.Services
{
    public class AddressService : BaseUserService, IAddressService
    {
        public AddressService(ICurrentUser user) : base(user)
        {
        }

        public async Task<IEnumerable<AddressModel>> GetAddressesByPerson(Guid personId)
        {
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var addresses = await unitOfWork.Addresses.GetAddressesByPerson(personId);

                return addresses.Select(a => new AddressModel(a)).ToList();
            }
        }
    }
}
