using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IPersonDietaryRequirementRepository : IReadWriteRepository<PersonDietaryRequirement>
    {
        Task<IEnumerable<PersonDietaryRequirement>> GetByPerson(int personId);
    }
}
