using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ConditionRepository : ReadRepository<Condition>, IConditionRepository
    {
        public ConditionRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}