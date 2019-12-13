using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class AspectTypeRepository : ReadRepository<AspectType>, IAspectTypeRepository
    {
        public AspectTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}