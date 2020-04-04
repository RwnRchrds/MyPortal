using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces
{
    public interface ITaskRepository : IReadWriteRepository<Models.Task>
    {
        Task<IEnumerable<Models.Task>> GetByPerson(Guid personId);
    }
}