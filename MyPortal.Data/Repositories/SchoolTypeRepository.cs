using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SchoolTypeRepository : ReadRepository<SchoolType>, ISchoolTypeRepository
    {
        public SchoolTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}