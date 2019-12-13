using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class DietaryRequirementRepository : ReadRepository<DietaryRequirement>, IDietaryRequirementRepository
    {
        public DietaryRequirementRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}