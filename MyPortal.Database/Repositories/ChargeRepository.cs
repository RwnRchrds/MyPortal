using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ChargeRepository : BaseReadWriteRepository<Charge>, IChargeRepository
    {
        public ChargeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "C")
        {
        }

        public async Task Update(Charge entity)
        {
            var charge = await Context.Charges.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (charge == null)
            {
                throw new EntityNotFoundException("Charge not found.");
            }

            charge.Name = entity.Name;
            charge.Code = entity.Code;
            charge.Amount = entity.Amount;
            charge.Description = entity.Description;
            charge.Active = entity.Active;
            charge.DefaultRecurrences = entity.DefaultRecurrences;
            charge.Variable = entity.Variable;
        }
    }
}
