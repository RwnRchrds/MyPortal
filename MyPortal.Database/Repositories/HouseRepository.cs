using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class HouseRepository : BaseStudentGroupRepository<House>, IHouseRepository
    {
        public HouseRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(House entity)
        {
            var house = await DbUser.Context.Houses.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (house == null)
            {
                throw new EntityNotFoundException("House not found.");
            }

            house.ColourCode = entity.ColourCode;
        }
    }
}