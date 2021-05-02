using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class PhoneNumberRepository : BaseReadWriteRepository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        public async Task Update(PhoneNumber entity)
        {
            var phoneNumber = await Context.PhoneNumbers.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (phoneNumber == null)
            {
                throw new EntityNotFoundException("Phone number not found.");
            }

            phoneNumber.TypeId = entity.TypeId;
            phoneNumber.Number = entity.Number;
            phoneNumber.Main = entity.Main;
        }
    }
}