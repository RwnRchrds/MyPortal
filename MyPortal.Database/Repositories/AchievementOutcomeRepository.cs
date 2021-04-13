using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AchievementOutcomeRepository : BaseReadWriteRepository<AchievementOutcome>, IAchievementOutcomeRepository
    {
        public AchievementOutcomeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task Update(AchievementOutcome entity)
        {
            var outcome = await Context.AchievementOutcomes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (outcome == null)
            {
                throw new EntityNotFoundException("Achievement outcome not found.");
            }

            if (outcome.System)
            {
                throw new SystemEntityException("System entities cannot be modified.");
            }

            outcome.Description = entity.Description;
            outcome.Active = entity.Active;
        }
    }
}