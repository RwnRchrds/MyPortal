using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class PersonConditionRepository : ReadWriteRepository<PersonCondition>, IPersonConditionRepository
    {
        public PersonConditionRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PersonCondition>> GetByPerson(int personId)
        {
            return await Context.PersonConditions.Where(x => x.PersonId == personId)
                .OrderBy(x => x.Condition.Description).ToListAsync();
        }
    }
}