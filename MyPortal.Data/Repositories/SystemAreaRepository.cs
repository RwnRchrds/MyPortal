using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SystemAreaRepository : ReadRepository<SystemArea>, ISystemAreaRepository
    {
        public SystemAreaRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}