using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class IntakeTypeRepository : ReadRepository<IntakeType>, IIntakeTypeRepository
    {
        public IntakeTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}