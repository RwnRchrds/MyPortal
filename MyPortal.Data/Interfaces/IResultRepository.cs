using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IResultRepository : IReadWriteRepository<Result>
    {
        Task<IEnumerable<Result>> GetByStudent(int studentId, int resultSetId);
        Task<IEnumerable<Result>> GetByAspect(int aspectId);
        Task<IEnumerable<Result>> GetByResultSet(int resultSetId);
    }
}
