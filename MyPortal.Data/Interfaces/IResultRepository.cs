using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IResultRepository : IReadWriteRepository<Result>
    {
        Task<IEnumerable<Result>> GetResultsByStudent(int studentId, int resultSetId);
    }
}
