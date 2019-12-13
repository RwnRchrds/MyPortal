using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class HouseRepository : ReadWriteRepository<House>, IHouseRepository
    {
        public HouseRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}