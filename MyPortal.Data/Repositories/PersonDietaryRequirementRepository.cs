using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class PersonDietaryRequirementRepository : ReadWriteRepository<PersonDietaryRequirement>, IPersonDietaryRequirementRepository
    {
        public PersonDietaryRequirementRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PersonDietaryRequirement>> GetByPerson(int personId)
        {
            return await Context.PersonDietaryRequirements.Where(x => x.PersonId == personId)
                .OrderBy(x => x.DietaryRequirement.Description).ToListAsync();
        }
    }
}