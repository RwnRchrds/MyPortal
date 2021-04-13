using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AccountTransactionRepository : BaseReadWriteRepository<AccountTransaction>, IAccountTransactionRepository
    {
        public AccountTransactionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "AT")
        {

        }
    }
}
