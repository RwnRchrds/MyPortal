using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
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

        public async Task CreateEmailAddress(EmailAddressDto emailAddress)
        {
            ValidationService.ValidateModel(emailAddress);

            UnitOfWork.EmailAddresses.Add(Mapper.Map<EmailAddress>(emailAddress));

            await UnitOfWork.Complete();
        }

        public async Task UpdateEmailAddress(EmailAddressDto emailAddress)
        {
            var emailInDb = await UnitOfWork.EmailAddresses.GetById(emailAddress.Id);

            if (emailInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Email address not found.");
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
                throw new ServiceException(ExceptionType.NotFound, "Email address not found.");
            }

            UnitOfWork.EmailAddresses.Remove(emailInDb);

            await UnitOfWork.Complete();
        }

        public async Task CreatePhoneNumber(PhoneNumberDto phoneNumber)
        {
            ValidationService.ValidateModel(phoneNumber);

            UnitOfWork.PhoneNumbers.Add(Mapper.Map<PhoneNumber>(phoneNumber));

            await UnitOfWork.Complete();
        }

        public async Task UpdatePhoneNumber(PhoneNumberDto phoneNumber)
        {
            var phoneNumberInDb = await UnitOfWork.PhoneNumbers.GetById(phoneNumber.Id);

            if (phoneNumberInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Phone number not found.");
            }

            phoneNumberInDb.TypeId = phoneNumber.TypeId;
            phoneNumberInDb.Number = phoneNumber.Number;
        }

        public async Task DeletePhoneNumber(int phoneNumberId)
        {
            var phoneNumber = await UnitOfWork.PhoneNumbers.GetById(phoneNumberId);

            if (phoneNumber == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Phone number not found.");
            }

            UnitOfWork.PhoneNumbers.Remove(phoneNumber);

            await UnitOfWork.Complete();
        }

        public async Task<EmailAddressDto> GetEmailAddressById(int emailAddressId)
        {
            var emailInDb = await UnitOfWork.EmailAddresses.GetById(emailAddressId);

            if (emailInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Email address not found.");
            }

            return Mapper.Map<EmailAddressDto>(emailInDb);
        }

        public async Task<IEnumerable<EmailAddressDto>> GetEmailAddressesByPerson(int personId)
        {
            return (await UnitOfWork.EmailAddresses.GetByPerson(personId)).Select(Mapper.Map<EmailAddressDto>);
        }

        public async Task<PhoneNumberDto> GetPhoneNumberById(int phoneNumberById)
        {
            var phoneNumber = await UnitOfWork.PhoneNumbers.GetById(phoneNumberById);

            if (phoneNumber == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Phone number not found.");
            }

            return Mapper.Map<PhoneNumberDto>(phoneNumber);
        }

        public async Task<IEnumerable<PhoneNumberDto>> GetPhoneNumbersByPerson(int personId)
        {
            return (await UnitOfWork.PhoneNumbers.GetByPerson(personId)).Select(Mapper.Map<PhoneNumberDto>);
        }

        public async Task CreateAddress(AddressDto address)
        {
            ValidationService.ValidateModel(address);

            UnitOfWork.Addresses.Add(Mapper.Map<Address>(address));

            await UnitOfWork.Complete();
        }

        public async Task UpdateAddress(AddressDto address)
        {
            var addressInDb = await UnitOfWork.Addresses.GetById(address.Id);

            if (addressInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Address not found.");
            }

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

        public async Task<AddressDto> GetAddressById(int addressId)
        {
            var address = await UnitOfWork.Addresses.GetById(addressId);

            if (address == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Address not found.");
            }

            return Mapper.Map<AddressDto>(address);
        }

        public async Task<AddressPersonDto> GetAddressPersonById(int addressPersonId)
        {
            var addressPerson = await UnitOfWork.AddressPersons.GetById(addressPersonId);

            if (addressPerson == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Person not found at address.");
            }

            return Mapper.Map<AddressPersonDto>(addressPerson);
        }

        public async Task AddPersonToAddress(AddressPersonDto addressPerson)
        {
            ValidationService.ValidateModel(addressPerson);

            UnitOfWork.AddressPersons.Add(Mapper.Map<AddressPerson>(addressPerson));

            await UnitOfWork.Complete();
        }

        public async Task RemovePersonFromAddress(int addressPersonId)
        {
            var addressPerson = await UnitOfWork.AddressPersons.GetById(addressPersonId);

            if (addressPerson == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Person not found at address.");
            }

            UnitOfWork.AddressPersons.Remove(addressPerson);

            await UnitOfWork.Complete();
        }

        public async Task DeleteAddress(int addressId)
        {
            var address = await UnitOfWork.Addresses.GetById(addressId);

            if (address == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Address not found.");
            }

            UnitOfWork.Addresses.Remove(address);

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<AddressDto>> GetAddressesByPerson(int personId)
        {
            return (await UnitOfWork.Addresses.GetAddressesByPerson(personId)).Select(Mapper.Map<AddressDto>);
        }

        public async Task<IDictionary<int, string>> GetPhoneNumberTypesLookup()
        {
            return (await UnitOfWork.PhoneNumberTypes.GetAll()).ToDictionary(x => x.Id, x => x.Description);
        }

    }
}