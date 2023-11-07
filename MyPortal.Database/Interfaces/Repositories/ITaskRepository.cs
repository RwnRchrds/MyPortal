using System;
using System.Collections.Generic;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ITaskRepository : IReadWriteRepository<Task>, IUpdateRepository<Task>
    {
        System.Threading.Tasks.Task<IEnumerable<Task>> GetByAssignedTo(Guid personId,
            TaskSearchOptions searchOptions = null);
    }
}