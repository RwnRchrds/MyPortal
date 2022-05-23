using System;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IResultRepository : IReadWriteRepository<Result>, IUpdateRepository<Result>
    {
        Task<Result> Get(Guid studentId, Guid aspectId, Guid resultSetId);
    }
}
