using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ConditionRepository : ReadRepository<MedicalCondition>, IConditionRepository
    {
        public ConditionRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}