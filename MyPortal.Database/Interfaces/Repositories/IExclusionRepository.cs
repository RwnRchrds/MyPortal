using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExclusionRepository : IReadWriteRepository<Exclusion>, IUpdateRepository<Exclusion>
    {
        Task<int> GetCountByStudent(Guid studentId);
        Task<IEnumerable<Exclusion>> GetByStudent(Guid studentId);
    }
}