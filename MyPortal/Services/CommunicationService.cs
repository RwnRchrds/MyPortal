using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Exceptions;
using MyPortal.Models.Database;

namespace MyPortal.Services
{
    public class CommunicationService
    {
        public static async Task CreateEmailAddress(CommunicationEmailAddress emailAddress, MyPortalDbContext context)
        {
            if (!ValidationService.ModelIsValid(emailAddress))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            context.CommunicationEmailAddresses.Add(emailAddress);

            await context.SaveChangesAsync();
        }

        public static async Task UpdateEmailAddress(CommunicationEmailAddress emailAddress, MyPortalDbContext context)
        {
            var emailInDb =
                await context.CommunicationEmailAddresses.SingleOrDefaultAsync(x => x.Id == emailAddress.Id);

            if (emailInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Email address not found");
            }

            emailInDb.Address = emailAddress.Address;
            emailInDb.Main = emailAddress.Main;
            emailInDb.Primary = emailAddress.Primary;
            emailInDb.Notes = emailAddress.Notes;
        }

        public static async Task DeleteEmailAddress(CommunicationEmailAddress emailAddress, MyPortalDbContext context)
        {
            var emailInDb = await context.CommunicationEmailAddresses.SingleOrDefaultAsync(x => x.Id == emailAddress.Id);

            if (emailInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Email address not found");
            }

            context.CommunicationEmailAddresses.Remove(emailInDb);

            await context.SaveChangesAsync();
        }

        public async Task<CommunicationEmailAddress> GetEmailAddressByIdModel(int emailAddressId, MyPortalDbContext context)
        {
            var emailInDb = await context.CommunicationEmailAddresses.SingleOrDefaultAsync(e => e.Id == emailAddressId);

            if (emailInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Email address not found");
            }

            return emailInDb;
        }

        public async Task<IEnumerable<CommunicationEmailAddress>> GetEmailAddressesByPersonModel(int personId,
            MyPortalDbContext context)
        {
            var emailAddresses =
                await context.CommunicationEmailAddresses.Where(x => x.PersonId == personId).ToListAsync();

            return emailAddresses;
        }
    }
}