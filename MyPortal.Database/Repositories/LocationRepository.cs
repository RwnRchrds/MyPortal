using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class LocationRepository : BaseReadWriteRepository<Location>, ILocationRepository
    {
        public LocationRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(Location entity)
        {
            var location = await DbUser.Context.Locations.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (location == null)
            {
                throw new EntityNotFoundException("Location not found.");
            }

            if (location.System)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }

            location.Description = entity.Description;
        }
    }
}
