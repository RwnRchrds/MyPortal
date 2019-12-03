using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Services
{
    public class CommunicationService : MyPortalService
    {
        public CommunicationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public CommunicationService() : base()
        {

        }

        public async Task CreateEmailAddress(CommunicationEmailAddress emailAddress)
        {
            if (!ValidationService.ModelIsValid(emailAddress))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.CommunicationEmailAddresses.Add(emailAddress);

            await UnitOfWork.Complete();
        }

        public async Task UpdateEmailAddress(CommunicationEmailAddress emailAddress)
        {
            var emailInDb = await UnitOfWork.CommunicationEmailAddresses.GetById(emailAddress.Id);

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
            var emailInDb = await UnitOfWork.CommunicationEmailAddresses.GetById(emailAddressId);

            if (emailInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Email address not found");
            }

            UnitOfWork.CommunicationEmailAddresses.Remove(emailInDb);

            await UnitOfWork.Complete();
        }

        public async Task CreatePhoneNumber(CommunicationPhoneNumber phoneNumber)
        {
            if (!ValidationService.ModelIsValid(phoneNumber))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.CommunicationPhoneNumbers.Add(phoneNumber);

            await UnitOfWork.Complete();
        }

        public async Task UpdatePhoneNumber(CommunicationPhoneNumber phoneNumber)
        {
            var phoneNumberInDb = await GetPhoneNumberById(phoneNumber.Id);

            phoneNumberInDb.TypeId = phoneNumber.TypeId;
            phoneNumberInDb.Number = phoneNumber.Number;
        }

        public async Task DeletePhoneNumber(int phoneNumberId)
        {
            var phoneNumber = await GetPhoneNumberById(phoneNumberId);

            UnitOfWork.CommunicationPhoneNumbers.Remove(phoneNumber);

            await UnitOfWork.Complete();
        }

        public async Task<CommunicationEmailAddress> GetEmailAddressById(int emailAddressId)
        {
            var emailInDb = await UnitOfWork.CommunicationEmailAddresses.GetById(emailAddressId);

            if (emailInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Email address not found");
            }

            return emailInDb;
        }

        public async Task<IEnumerable<CommunicationEmailAddress>> GetEmailAddressesByPerson(int personId)
        {
            var emailAddresses =
                await UnitOfWork.CommunicationEmailAddresses.GetByPerson(personId);

            return emailAddresses;
        }

        public async Task<CommunicationPhoneNumber> GetPhoneNumberById(int phoneNumberById)
        {
            var phoneNumber = await UnitOfWork.CommunicationPhoneNumbers.GetById(phoneNumberById);

            if (phoneNumber == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Phone number not found");
            }

            return phoneNumber;
        }

        public async Task<IEnumerable<CommunicationPhoneNumber>> GetPhoneNumbersByPerson(int personId)
        {
            var phoneNumbers = await UnitOfWork.CommunicationPhoneNumbers.GetByPerson(personId);

            return phoneNumbers;
        }

        public async Task CreateAddress(CommunicationAddress address)
        {
            if (!ValidationService.ModelIsValid(address))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.CommunicationAddresses.Add(address);

            await UnitOfWork.Complete();
        }

        public async Task UpdateAddress(CommunicationAddress address)
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

        public async Task<CommunicationAddress> GetAddressById(int addressId)
        {
            var address = await UnitOfWork.CommunicationAddresses.GetById(addressId);

            if (address == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Address not found");
            }

            return address;
        }

        public async Task<CommunicationAddressPerson> GetAddressPersonById(int addressPersonId)
        {
            var addressPerson = await UnitOfWork.CommunicationAddressPersons.GetById(addressPersonId);

            if (addressPerson == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Person not found at address");
            }

            return addressPerson;
        }

        public async Task AddPersonToAddress(CommunicationAddressPerson addressPerson)
        {
            if (!ValidationService.ModelIsValid(addressPerson))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.CommunicationAddressPersons.Add(addressPerson);

            await UnitOfWork.Complete();
        }

        public async Task RemovePersonFromAddress(int addressPersonId)
        {
            var addressPerson = await GetAddressPersonById(addressPersonId);

            UnitOfWork.CommunicationAddressPersons.Remove(addressPerson);

            await UnitOfWork.Complete();
        }

        public async Task DeleteAddress(int addressId)
        {
            var address = await GetAddressById(addressId);

            UnitOfWork.CommunicationAddresses.Remove(address);

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<CommunicationAddress>> GetAddressesByPerson(int personId)
        {
            var addresses = await UnitOfWork.CommunicationAddresses.GetAddressesByPerson(personId);

            return addresses;
        }

        public async Task<IDictionary<int, string>> GetPhoneNumberTypesLookup()
        {
            var phoneNumberTypes = await UnitOfWork.CommunicationPhoneNumberTypes.GetAll();

            return phoneNumberTypes.ToDictionary(x => x.Id, x => x.Description);
        }

    }
}