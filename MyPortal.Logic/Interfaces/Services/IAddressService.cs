using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.Addresses;
using MyPortal.Logic.Models.Data.Contacts;
using MyPortal.Logic.Models.Requests.Addresses;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAddressService : IService
    {
        Task<IEnumerable<AddressModel>> GetMatchingAddresses(AddressSearchRequestModel searchModel);
        Task CreateAddressForPerson(Guid personId, EntityAddressRequestModel model);
        Task CreateAddressForAgency(Guid agencyId, EntityAddressRequestModel model);
        Task UpdateAddressLinkForPerson(Guid addressPersonId, LinkAddressRequestModel model);
        Task UpdateAddressLinkForAgency(Guid addressAgencyId, LinkAddressRequestModel model);
        Task UpdateAddress(Guid addressId, AddressRequestModel model);
        Task LinkAddressToPerson(LinkAddressRequestModel model);
        Task LinkAddressToAgency(LinkAddressRequestModel model);
        Task<IEnumerable<AddressLinkDataModel>> GetAddressLinksByPerson(Guid personId);
        Task<IEnumerable<AddressLinkDataModel>> GetAddressLinksByAgency(Guid agencyId);
        Task<LinkedAddressesDataModel> GetAddressLinksByAddress(Guid addressId);
        Task DeleteAddressLinkForPerson(Guid addressPersonId);
        Task DeleteAddressLinkForAgency(Guid addressAgencyId);
    }
}