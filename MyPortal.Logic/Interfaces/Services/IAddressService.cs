using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Data.Contacts;


namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAddressService : IService
    {
        Task<IEnumerable<AddressModel>> GetAddressesByPerson(Guid personId);
        Task<IEnumerable<AddressModel>> GetMatchingAddresses(AddressSearchOptions searchOptions);
    }
}
