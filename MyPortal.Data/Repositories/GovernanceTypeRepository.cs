using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class GovernanceTypeRepository : ReadRepository<GovernanceType>, IGovernanceTypeRepository
    {
        public GovernanceTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}