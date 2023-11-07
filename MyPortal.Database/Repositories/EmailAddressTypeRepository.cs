using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class EmailAddressTypeRepository : BaseReadRepository<EmailAddressType>, IEmailAddressTypeRepository
    {
        public EmailAddressTypeRepository(DbUser dbUser) : base(dbUser)
        {
        }
    }
}