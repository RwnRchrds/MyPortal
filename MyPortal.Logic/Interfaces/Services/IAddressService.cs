using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.Contacts;


namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressModel>> GetAddressesByPerson(Guid personId);
    }
}
