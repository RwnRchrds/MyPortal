using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            var emailInDb = await UnitOfWork.CommunicationEmailAddresses.GetByIdAsync(emailAddress.Id);

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
            var emailInDb = await UnitOfWork.CommunicationEmailAddresses.GetByIdAsync(emailAddressId);

            if (emailInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Email address not found");
            }

            UnitOfWork.CommunicationEmailAddresses.Remove(emailInDb);

            await UnitOfWork.Complete();
        }

        public async Task<CommunicationEmailAddress> GetEmailAddressById(int emailAddressId)
        {
            var emailInDb = await UnitOfWork.CommunicationEmailAddresses.GetByIdAsync(emailAddressId);

            if (emailInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Email address not found");
            }

            return emailInDb;
        }

        public async Task<IEnumerable<CommunicationEmailAddress>> GetEmailAddressesByPerson(int personId)
        {
            var emailAddresses =
                await UnitOfWork.CommunicationEmailAddresses.GetEmailAddressesByPerson(personId);

            return emailAddresses;
        }
    }
}