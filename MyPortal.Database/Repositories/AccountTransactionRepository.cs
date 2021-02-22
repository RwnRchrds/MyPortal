using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class AccountTransactionRepository : BaseReadWriteRepository<AccountTransaction>, IAccountTransactionRepository
    {
        public AccountTransactionRepository(ApplicationDbContext context) : base(context, "AT")
        {

        }
    }
}
