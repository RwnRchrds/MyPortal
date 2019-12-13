using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IPersonConditionRepository : IReadWriteRepository<PersonCondition>
    {
        Task<IEnumerable<PersonCondition>> GetByPerson(int personId);
    }
}
