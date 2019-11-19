using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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

        public async Task CreateAddress(CommunicationAddress address)
        {
            if (!ValidationService.ModelIsValid(address))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.CommunicationAddresses.Add(address);

            await UnitOfWork.Complete();
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


    }
}