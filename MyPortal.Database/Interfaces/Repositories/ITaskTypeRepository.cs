using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ITaskTypeRepository : IReadWriteRepository<TaskType>, IUpdateRepository<TaskType>
    {
        Task<IEnumerable<TaskType>> GetAll(bool personalOnly, bool activeOnly, bool includeSystem);
    }
}