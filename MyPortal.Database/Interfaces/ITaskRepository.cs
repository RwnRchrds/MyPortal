using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces
{
    public interface ITaskRepository : IReadWriteRepository<Models.Task>
    {
        Task<IEnumerable<Models.Task>> GetByAssignedTo(Guid personId);
        Task<IEnumerable<Models.Task>> GetActiveByAssignedTo(Guid personId);
        Task<IEnumerable<Models.Task>> GetCompletedByAssignedTo(Guid personId);
        Task<IEnumerable<Models.Task>> GetOverdueByAssignedTo(Guid personId);
    }
}