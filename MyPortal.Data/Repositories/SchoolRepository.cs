using System.Data.Entity;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SchoolRepository : ReadWriteRepository<School>, ISchoolRepository
    {
        public SchoolRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<School> GetLocal()
        {
            return await Context.SystemSchools.SingleOrDefaultAsync(x => x.Local);
        }
    }
}