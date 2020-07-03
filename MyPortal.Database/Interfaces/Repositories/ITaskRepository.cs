using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Search;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ITaskRepository : IReadWriteRepository<Models.Task>
    {
        Task<IEnumerable<Models.Task>> GetByAssignedTo(Guid personId, TaskSearchOptions searchOptions = null);
    }
}