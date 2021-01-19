using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ITaskTypeRepository : IReadWriteRepository<TaskType>
    {
        Task<IEnumerable<TaskType>> GetAll(bool personal, bool active, bool includeReserved);
    }
}
