using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DietaryRequirementRepository : BaseReadWriteRepository<DietaryRequirement>,
        IDietaryRequirementRepository
    {
        public DietaryRequirementRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(DietaryRequirement entity)
        {
            var dietaryRequirement =
                await DbUser.Context.DietaryRequirements.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (dietaryRequirement == null)
            {
                throw new EntityNotFoundException("Dietary requirement not found.");
            }

            dietaryRequirement.Description = entity.Description;
            dietaryRequirement.Active = entity.Active;
        }
    }
}