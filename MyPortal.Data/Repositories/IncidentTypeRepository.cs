using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class IncidentTypeRepository : ReadRepository<IncidentType>, IIncidentTypeRepository
    {
        public IncidentTypeRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<IncidentType>> GetRecorded(int academicYearId)
        {
            return await Context.IncidentTypes
                .Where(x => x.Incidents.Any(i => i.AcademicYearId == academicYearId)).ToListAsync();
        }
    }
}