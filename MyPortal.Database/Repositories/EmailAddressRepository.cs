using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class EmailAddressRepository : BaseReadWriteRepository<EmailAddress>, IEmailAddressRepository
    {
        public EmailAddressRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task Update(EmailAddress entity)
        {
            var emailAddress = await Context.EmailAddresses.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (emailAddress == null)
            {
                throw new EntityNotFoundException("Email address not found.");
            }

            emailAddress.Address = entity.Address;
            emailAddress.Main = entity.Main;
            emailAddress.Notes = entity.Notes;
            emailAddress.TypeId = entity.TypeId;
        }
    }
}