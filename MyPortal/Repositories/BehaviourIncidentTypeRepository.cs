using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class BehaviourIncidentTypeRepository : ReadOnlyRepository<BehaviourIncidentType>, IBehaviourIncidentTypeRepository
    {
        public BehaviourIncidentTypeRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<BehaviourIncidentType>> GetRecorded(int academicYearId)
        {
            return await Context.BehaviourIncidentTypes
                .Where(x => x.Incidents.Any(i => i.AcademicYearId == academicYearId)).ToListAsync();
        }
    }
}