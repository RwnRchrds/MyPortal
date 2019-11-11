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
    public class PersonnelObservationRepository : Repository<PersonnelObservation>, IPersonnelObservationRepository
    {
        public PersonnelObservationRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PersonnelObservation>> GetByStaffMember(int staffId)
        {
            return await Context.PersonnelObservations.Where(x => x.ObserveeId == staffId).ToListAsync();
        }
    }
}