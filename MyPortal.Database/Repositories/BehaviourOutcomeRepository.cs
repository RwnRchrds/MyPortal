using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class BehaviourOutcomeRepository : BaseReadWriteRepository<BehaviourOutcome>, IBehaviourOutcomeRepository
    {
        public BehaviourOutcomeRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(BehaviourOutcome entity)
        {
            var outcome = await DbUser.Context.BehaviourOutcomes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (outcome == null)
            {
                throw new EntityNotFoundException("Behaviour outcome not found.");
            }

            outcome.Description = entity.Description;
            outcome.Active = entity.Active;
        }
    }
}