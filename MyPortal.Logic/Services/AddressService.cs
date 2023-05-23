using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Addresses;
using MyPortal.Logic.Models.Data.Contacts;
using MyPortal.Logic.Models.Requests.Addresses;
using Task = System.Threading.Tasks.Task;


namespace MyPortal.Logic.Services
{
    public class AddressService : BaseUserService, IAddressService
    {
        public AddressService(ISessionUser user) : base(user)
        {
        }

        private Address CreateAddress(EntityAddressRequestModel model)
        {
            return new Address
            {
                Id = Guid.NewGuid(),
                Apartment = model.Apartment,
                BuildingName = model.BuildingName,
                BuildingNumber = model.BuildingNumber,
                Street = model.Street,
                District = model.District,
                Town = model.Town,
                County = model.County,
                Postcode = model.Postcode,
                Country = model.Country
            };
        }

        public async Task<IEnumerable<AddressModel>> GetMatchingAddresses(AddressSearchRequestModel searchModel)
        {
            await using var unitOfWork = await User.GetConnection();

            var searchOptions = searchModel.GetSearchOptions();

            var matchingAddresses = await unitOfWork.Addresses.GetAll(searchOptions);

            return matchingAddresses.Select(a => new AddressModel(a)).ToArray();
        }

        public async Task CreateAddressForPerson(Guid personId, EntityAddressRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();

            var person = await unitOfWork.People.GetById(personId);

            if (person == null)
            {
                throw new NotFoundException("Person not found.");
            }
            
            if (model.Main)
            {
                var personAddresses = await unitOfWork.AddressPeople.GetByPerson(person.Id);

                foreach (var linkedAddress in personAddresses)
                {
                    linkedAddress.Main = false;
                    await unitOfWork.AddressPeople.Update(linkedAddress);
                }
            }

            person.AddressPeople.Add(new AddressPerson
            {
                Id = Guid.NewGuid(),
                Address = CreateAddress(model),
                AddressTypeId = model.AddressTypeId,
                Main = model.Main
            });
            
            await unitOfWork.SaveChangesAsync();
        }

        public async Task CreateAddressForAgency(Guid agencyId, EntityAddressRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();

            var agency = await unitOfWork.Agencies.GetById(agencyId);

            if (agency == null)
            {
                throw new NotFoundException("Agency not found.");
            }

            if (model.Main)
            {
                var agencyAddresses = await unitOfWork.AddressAgencies.GetByAgency(agency.Id);

                foreach (var linkedAddress in agencyAddresses)
                {
                    linkedAddress.Main = false;
                    await unitOfWork.AddressAgencies.Update(linkedAddress);
                }
            }
            
            agency.AddressAgencies.Add(new AddressAgency
            {
                Id = Guid.NewGuid(),
                Address = CreateAddress(model),
                AddressTypeId = model.AddressTypeId,
                Main = model.Main
            });

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAddressLinkForPerson(Guid addressPersonId, LinkAddressRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();
            var addressPerson = await unitOfWork.AddressPeople.GetById(addressPersonId);

            if (addressPerson == null)
            {
                throw new NotFoundException("Linked address not found.");
            }

            addressPerson.AddressId = model.AddressId;
            addressPerson.AddressTypeId = model.AddressTypeId;
            addressPerson.Main = model.Main;

            await unitOfWork.AddressPeople.Update(addressPerson);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAddressLinkForAgency(Guid addressAgencyId, LinkAddressRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();
            var addressAgency = await unitOfWork.AddressAgencies.GetById(addressAgencyId);

            if (addressAgency == null)
            {
                throw new NotFoundException("Linked address not found.");
            }

            addressAgency.AddressId = model.AddressId;
            addressAgency.AddressTypeId = model.AddressTypeId;
            addressAgency.Main = model.Main;

            await unitOfWork.AddressAgencies.Update(addressAgency);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAddress(Guid addressId, AddressRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();

            var address = await unitOfWork.Addresses.GetById(addressId);

            if (address == null)
            {
                throw new EntityNotFoundException("Address not found.");
            }

            address.Apartment = model.Apartment;
            address.BuildingName = model.BuildingName;
            address.BuildingNumber = model.BuildingNumber;
            address.Street = model.Street;
            address.District = model.District;
            address.Town = model.Town;
            address.County = model.County;
            address.Postcode = model.Postcode;
            address.Country = model.Country;

            await unitOfWork.Addresses.Update(address);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task LinkAddressToPerson(LinkAddressRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();

            var address = await unitOfWork.Addresses.GetById(model.AddressId);

            if (address == null)
            {
                throw new NotFoundException("Address not found.");
            }

            var person = await unitOfWork.People.GetById(model.EntityId);

            if (person == null)
            {
                throw new NotFoundException("Person not found.");
            }

            var addressPerson = new AddressPerson
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                PersonId = person.Id,
                AddressTypeId = model.AddressTypeId,
                Main = model.Main
            };
            
            unitOfWork.AddressPeople.Create(addressPerson);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task LinkAddressToAgency(LinkAddressRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();

            var address = await unitOfWork.Addresses.GetById(model.AddressId);

            if (address == null)
            {
                throw new NotFoundException("Address not found.");
            }

            var agency = await unitOfWork.Agencies.GetById(model.EntityId);

            if (agency == null)
            {
                throw new NotFoundException("Agency not found.");
            }

            var addressAgency = new AddressAgency
            {
                Id = Guid.NewGuid(),
                AddressId = address.Id,
                AgencyId = agency.Id,
                AddressTypeId = model.AddressTypeId,
                Main = model.Main
            };
            
            unitOfWork.AddressAgencies.Create(addressAgency);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<AddressLinkDataModel>> GetAddressLinksByPerson(Guid personId)
        {
            await using var unitOfWork = await User.GetConnection();
            var addressPeople = await unitOfWork.AddressPeople.GetByPerson(personId);
            var results = addressPeople.Select(ap => new AddressLinkDataModel(ap)).ToArray();
            
            return results;
        }

        public async Task<IEnumerable<AddressLinkDataModel>> GetAddressLinksByAgency(Guid agencyId)
        {
            await using var unitOfWork = await User.GetConnection();
            var addressAgencies = await unitOfWork.AddressAgencies.GetByAgency(agencyId);
            var results = addressAgencies.Select(ap => new AddressLinkDataModel(ap)).ToArray();
            
            return results;
        }

        public async Task<LinkedAddressesDataModel> GetAddressLinksByAddress(Guid addressId)
        {
            await using var unitOfWork = await User.GetConnection();
            var addressPeople = await unitOfWork.AddressPeople.GetByAddress(addressId);
            var addressAgencies = await unitOfWork.AddressAgencies.GetByAddress(addressId);

            return new LinkedAddressesDataModel
            {
                PersonLinks = addressPeople.Select(ap => new AddressLinkDataModel(ap)).ToArray(),
                AgencyLinks = addressAgencies.Select(al => new AddressLinkDataModel(al)).ToArray()
            };
        }
        
        private async Task DeleteAddressIfUnused(Guid addressId)
        {
            await using var unitOfWork = await User.GetConnection();
            var addressLinks = await GetAddressLinksByAddress(addressId);
            
            var numberOfLinks = addressLinks.PersonLinks.Length + addressLinks.AgencyLinks.Length;

            if (numberOfLinks <= 0)
            {
                // Delete unlinked address from database to prevent cluttering db with unused addresses
                await unitOfWork.Addresses.Delete(addressId);
            }
        }

        public async Task DeleteAddressLinkForPerson(Guid addressPersonId)
        {
            Guid addressId;
            
            await using (var unitOfWork = await User.GetConnection())
            {
                var addressPerson = await unitOfWork.AddressPeople.GetById(addressPersonId);

                if (addressPerson == null)
                {
                    throw new EntityNotFoundException("Linked address not found.");
                }

                addressId = addressPerson.AddressId;

                await unitOfWork.AddressPeople.Delete(addressPerson.Id);

                await unitOfWork.SaveChangesAsync();
            }
            
            await DeleteAddressIfUnused(addressId);
        }

        public async Task DeleteAddressLinkForAgency(Guid addressAgencyId)
        {
            Guid addressId;
            
            await using (var unitOfWork = await User.GetConnection())
            {
                var addressAgency = await unitOfWork.AddressAgencies.GetById(addressAgencyId);

                if (addressAgency == null)
                {
                    throw new EntityNotFoundException("Linked address not found.");
                }

                addressId = addressAgency.AddressId;

                await unitOfWork.AddressAgencies.Delete(addressAgency.Id);

                await unitOfWork.SaveChangesAsync();
            }
            
            await DeleteAddressIfUnused(addressId);
        }
    }
}
