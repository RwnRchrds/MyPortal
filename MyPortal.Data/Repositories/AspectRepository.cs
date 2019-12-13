using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class AspectRepository : ReadWriteRepository<Aspect>, IAspectRepository
    {
        public AspectRepository(MyPortalDbContext context) : base (context)
        {

        }
    }
}