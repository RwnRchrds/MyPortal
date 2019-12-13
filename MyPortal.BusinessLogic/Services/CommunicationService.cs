using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class CommunicationService : MyPortalService
    {
        public CommunicationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public CommunicationService() : base()
        {

        }

        public async Task CreateEmailAddress(EmailAddress emailAddress)
        {
            ValidationService.ValidateModel(emailAddress);

            UnitOfWork.EmailAddresses.Add(emailAddress);

            await UnitOfWork.Complete();
        }

        public async Task UpdateEmailAddress(EmailAddress emailAddress)
        {
            var emailInDb = await UnitOfWork.EmailAddresses.GetById(emailAddress.Id);

            if (emailInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Email address not found");
            }

            emailInDb.Address = emailAddress.Address;
            emailInDb.Main = emailAddress.Main;
            emailInDb.Primary = emailAddress.Primary;
            emailInDb.Notes = emailAddress.Notes;

            await UnitOfWork.Complete();
        }

        public async Task DeleteEmailAddress(int emailAddressId)
        {
            var emailInDb = await UnitOfWork.EmailAddresses.GetById(emailAddressId);

            if (emailInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Email address not found");
            }

            UnitOfWork.EmailAddresses.Remove(emailInDb);

            await UnitOfWork.Complete();
        }

        public async Task CreatePhoneNumber(PhoneNumber phoneNumber)
        {
            ValidationService.ValidateModel(phoneNumber);

            UnitOfWork.PhoneNumbers.Add(phoneNumber);

            await UnitOfWork.Complete();
        }

        public async Task UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            var phoneNumberInDb = await GetPhoneNumberById(phoneNumber.Id);

            phoneNumberInDb.TypeId = phoneNumber.TypeId;
            phoneNumberInDb.Number = phoneNumber.Number;
        }

        public async Task DeletePhoneNumber(int phoneNumberId)
        {
            var phoneNumber = await GetPhoneNumberById(phoneNumberId);

            UnitOfWork.PhoneNumbers.Remove(phoneNumber);

            await UnitOfWork.Complete();
        }

        public async Task<EmailAddress> GetEmailAddressById(int emailAddressId)
        {
            var emailInDb = await UnitOfWork.EmailAddresses.GetById(emailAddressId);

            if (emailInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Email address not found");
            }

            return emailInDb;
        }

        public async Task<IEnumerable<EmailAddress>> GetEmailAddressesByPerson(int personId)
        {
            var emailAddresses =
                await UnitOfWork.EmailAddresses.GetByPerson(personId);

            return emailAddresses;
        }

        public async Task<PhoneNumber> GetPhoneNumberById(int phoneNumberById)
        {
            var phoneNumber = await UnitOfWork.PhoneNumbers.GetById(phoneNumberById);

            if (phoneNumber == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Phone number not found");
            }

            return phoneNumber;
        }

        public async Task<IEnumerable<PhoneNumber>> GetPhoneNumbersByPerson(int personId)
        {
            var phoneNumbers = await UnitOfWork.PhoneNumbers.GetByPerson(personId);

            return phoneNumbers;
        }

        public async Task CreateAddress(Address address)
        {
            ValidationService.ValidateModel(address);

            UnitOfWork.Addresses.Add(address);

            await UnitOfWork.Complete();
        }

        public async Task UpdateAddress(Address address)
        {
            var addressInDb = await GetAddressById(address.Id);

            addressInDb.Apartment = address.Apartment;
            addressInDb.Country = address.Country;
            addressInDb.County = address.County;
            addressInDb.District = address.District;
            addressInDb.HouseName = address.HouseName;
            addressInDb.HouseNumber = address.HouseName;
            addressInDb.Postcode = address.Postcode;
            addressInDb.Street = address.Street;
            addressInDb.Town = address.Town;
            addressInDb.Validated = address.Validated;
        }

        public async Task<Address> GetAddressById(int addressId)
        {
            var address = await UnitOfWork.Addresses.GetById(addressId);

            if (address == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Address not found");
            }

            return address;
        }

        public async Task<AddressPerson> GetAddressPersonById(int addressPersonId)
        {
            var addressPerson = await UnitOfWork.AddressPersons.GetById(addressPersonId);

            if (addressPerson == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Person not found at address");
            }

            return addressPerson;
        }

        public async Task AddPersonToAddress(AddressPerson addressPerson)
        {
            ValidationService.ValidateModel(addressPerson);

            UnitOfWork.AddressPersons.Add(addressPerson);

            await UnitOfWork.Complete();
        }

        public async Task RemovePersonFromAddress(int addressPersonId)
        {
            var addressPerson = await GetAddressPersonById(addressPersonId);

            UnitOfWork.AddressPersons.Remove(addressPerson);

            await UnitOfWork.Complete();
        }

        public async Task DeleteAddress(int addressId)
        {
            var address = await GetAddressById(addressId);

            UnitOfWork.Addresses.Remove(address);

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<Address>> GetAddressesByPerson(int personId)
        {
            var addresses = await UnitOfWork.Addresses.GetAddressesByPerson(personId);

            return addresses;
        }

        public async Task<IDictionary<int, string>> GetPhoneNumberTypesLookup()
        {
            var phoneNumberTypes = await UnitOfWork.PhoneNumberTypes.GetAll();

            return phoneNumberTypes.ToDictionary(x => x.Id, x => x.Description);
        }

    }
}