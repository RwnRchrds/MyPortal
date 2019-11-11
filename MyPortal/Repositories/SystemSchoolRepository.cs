using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Persistence;

namespace MyPortal.Repositories
{
    public class SystemSchoolRepository : Repository<School>, ISystemSchoolRepository
    {
        public SystemSchoolRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<School> GetLocal()
        {
            return await Context.SystemSchools.SingleOrDefaultAsync(x => x.Local);
        }
    }
}