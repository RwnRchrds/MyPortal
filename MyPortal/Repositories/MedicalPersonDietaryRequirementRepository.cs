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
    public class MedicalPersonDietaryRequirementRepository : Repository<MedicalPersonDietaryRequirement>, IMedicalPersonDietaryRequirementRepository
    {
        public MedicalPersonDietaryRequirementRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<MedicalPersonDietaryRequirement>> GetByPerson(int personId)
        {
            return await Context.MedicalPersonDietaryRequirements.Where(x => x.PersonId == personId)
                .OrderBy(x => x.DietaryRequirement.Description).ToListAsync();
        }
    }
}