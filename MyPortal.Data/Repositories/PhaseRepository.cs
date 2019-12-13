using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class PhaseRepository : ReadRepository<Phase>, IPhaseRepository
    {
        public PhaseRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}