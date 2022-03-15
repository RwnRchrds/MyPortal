using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExclusionReasonRepository : BaseReadWriteRepository<ExclusionReason>, IExclusionReasonRepository
    {
        public ExclusionReasonRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(ExclusionReason entity)
        {
            var exclusionReason = await Context.ExclusionReasons.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (exclusionReason == null)
            {
                throw new EntityNotFoundException("Exclusion reason not found.");
            }

            if (exclusionReason.System)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }

            exclusionReason.Description = entity.Description;
            exclusionReason.Active = entity.Active;
        }
    }
}