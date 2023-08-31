using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class MedicalConditionRepository : BaseReadWriteRepository<MedicalCondition>, IMedicalConditionRepository
    {
        public MedicalConditionRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(MedicalCondition entity)
        {
            var medCondition = await DbUser.Context.Conditions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (medCondition == null)
            {
                throw new EntityNotFoundException("Medical condition not found.");
            }

            medCondition.Description = entity.Description;
            medCondition.Active = entity.Active;
        }
    }
}