using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class RegGroupRepository : ReadWriteRepository<RegGroup>, IRegGroupRepository
    {
        public RegGroupRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<RegGroup>> GetByYearGroup(int yearGroupId)
        {
            return await Context.RegGroups.Where(x => x.YearGroupId == yearGroupId).OrderBy(x => x.Name).ToListAsync();
        }
    }
}