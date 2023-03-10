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
    public class LocationRepository : BaseReadWriteRepository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(Location entity)
        {
            var location = await Context.Locations.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (location == null)
            {
                throw new EntityNotFoundException("Location not found.");
            }

            if (location.System)
            {
                throw new SystemEntityException("System entities cannot be modified.");
            }

            location.Description = entity.Description;
        }
    }
}
