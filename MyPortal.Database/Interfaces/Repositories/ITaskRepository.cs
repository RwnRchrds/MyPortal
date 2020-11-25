using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using Task = MyPortal.Database.Models.Entity.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ITaskRepository : IReadWriteRepository<Task>
    {
        Task<IEnumerable<Task>> GetByAssignedTo(Guid personId, TaskSearchOptions searchOptions = null);
    }
}