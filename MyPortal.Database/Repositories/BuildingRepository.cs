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
    public class BuildingRepository : BaseReadWriteRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(Building entity)
        {
            var building = await Context.Buildings.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (building == null)
            {
                throw new EntityNotFoundException("Building not found.");
            }

            building.Description = entity.Description;
            building.Active = entity.Active;
        }
    }
}