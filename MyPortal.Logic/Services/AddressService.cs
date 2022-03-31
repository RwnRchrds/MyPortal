using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class AddressService : BaseService, IAddressService
    {
        public async Task<IEnumerable<AddressModel>> GetAddressesByPerson(Guid personId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var addresses = await unitOfWork.Addresses.GetAddressesByPerson(personId);

                return addresses.Select(a => new AddressModel(a)).ToList();
            }
        }
    }
}
