using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class BehaviourOutcomeRepository : BaseReadWriteRepository<BehaviourOutcome>, IBehaviourOutcomeRepository
    {
        public BehaviourOutcomeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(BehaviourOutcome entity)
        {
            var outcome = await Context.BehaviourOutcomes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (outcome == null)
            {
                throw new EntityNotFoundException("Behaviour outcome not found.");
            }

            outcome.Description = entity.Description;
            outcome.Active = entity.Active;
        }
    }
}