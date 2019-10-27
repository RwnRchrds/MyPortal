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
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            _unitOfWork.CommunicationEmailAddresses.Add(emailAddress);

            await _unitOfWork.Complete();
        }

        public async Task UpdateEmailAddress(CommunicationEmailAddress emailAddress)
        {
            var emailInDb = await _unitOfWork.CommunicationEmailAddresses.GetByIdAsync(emailAddress.Id);

            if (emailInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Email address not found");
            }

            emailInDb.Address = emailAddress.Address;
            emailInDb.Main = emailAddress.Main;
            emailInDb.Primary = emailAddress.Primary;
            emailInDb.Notes = emailAddress.Notes;

            await _unitOfWork.Complete();
        }

        public async Task DeleteEmailAddress(int emailAddressId)
        {
            var emailInDb = await _unitOfWork.CommunicationEmailAddresses.GetByIdAsync(emailAddressId);

            if (emailInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Email address not found");
            }

            _unitOfWork.CommunicationEmailAddresses.Remove(emailInDb);

            await _unitOfWork.Complete();
        }

        public async Task<CommunicationEmailAddress> GetEmailAddressById(int emailAddressId)
        {
            var emailInDb = await _unitOfWork.CommunicationEmailAddresses.GetByIdAsync(emailAddressId);

            if (emailInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Email address not found");
            }

            return emailInDb;
        }

        public async Task<IEnumerable<CommunicationEmailAddress>> GetEmailAddressesByPerson(int personId)
        {
            var emailAddresses =
                await _unitOfWork.CommunicationEmailAddresses.GetEmailAddressesByPerson(personId);

            return emailAddresses;
        }
    }
}