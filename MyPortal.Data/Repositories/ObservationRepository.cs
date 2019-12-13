using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ObservationRepository : ReadWriteRepository<Observation>, IObservationRepository
    {
        public ObservationRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Observation>> GetByStaffMember(int staffId)
        {
            return await Context.Observations.Where(x => x.ObserveeId == staffId).ToListAsync();
        }
    }
}