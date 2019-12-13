using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IResultSetRepository : IReadWriteRepository<ResultSet>
    {
        Task<IEnumerable<ResultSet>> GetResultSetsByStudent(int studentId);
    }
}
