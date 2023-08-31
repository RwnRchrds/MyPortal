using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class RoomClosureReasonRepository : BaseReadWriteRepository<RoomClosureReason>, IRoomClosureReasonRepository
    {
        public RoomClosureReasonRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(RoomClosureReason entity)
        {
            var reason = await DbUser.Context.RoomClosureReasons.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (reason == null)
            {
                throw new EntityNotFoundException("Reason not found.");
            }

            reason.Description = entity.Description;
            reason.Active = entity.Active;
        }
    }
}