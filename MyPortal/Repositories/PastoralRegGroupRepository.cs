using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using Syncfusion.EJ2.Linq;

namespace MyPortal.Repositories
{
    public class PastoralRegGroupRepository : ReadWriteRepository<PastoralRegGroup>, IPastoralRegGroupRepository
    {
        public PastoralRegGroupRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PastoralRegGroup>> GetByYearGroup(int yearGroupId)
        {
            return await Context.PastoralRegGroups.Where(x => x.YearGroupId == yearGroupId).OrderBy(x => x.Name).ToListAsync();
        }
    }
}