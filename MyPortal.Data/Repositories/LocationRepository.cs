using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class LocationRepository : ReadRepository<Location>, ILocationRepository
    {
        public LocationRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}