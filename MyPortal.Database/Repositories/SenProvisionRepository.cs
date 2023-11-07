using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SenProvisionRepository : BaseReadWriteRepository<SenProvision>, ISenProvisionRepository
    {
        public SenProvisionRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(SenProvision entity)
        {
            var senProvision = await DbUser.Context.SenProvisions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (senProvision == null)
            {
                throw new EntityNotFoundException("SEN provision not found.");
            }

            senProvision.ProvisionTypeId = entity.ProvisionTypeId;
            senProvision.StartDate = entity.StartDate;
            senProvision.EndDate = entity.EndDate;
            senProvision.Note = entity.Note;
        }
    }
}