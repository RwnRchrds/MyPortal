using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Constants;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AchievementOutcomeRepository : BaseReadWriteRepository<AchievementOutcome>,
        IAchievementOutcomeRepository
    {
        public AchievementOutcomeRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(AchievementOutcome entity)
        {
            var outcome = await DbUser.Context.AchievementOutcomes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            var oldValue = JsonConvert.SerializeObject(entity);

            if (outcome == null)
            {
                throw new EntityNotFoundException("Achievement outcome not found.");
            }

            outcome.Description = entity.Description;
            outcome.Active = entity.Active;

            WriteAuditRaw(entity.Id, AuditActions.Update, oldValue);
        }
    }
}