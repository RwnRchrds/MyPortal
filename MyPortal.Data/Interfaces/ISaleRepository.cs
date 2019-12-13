using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface ISaleRepository : IReadWriteRepository<Sale>
    {
        Task<IEnumerable<Sale>> GetAllAsync(int academicYearId);
        Task<IEnumerable<Sale>> GetByStudent(int studentId, int academicYearId);

        Task<IEnumerable<Sale>> GetPending(int academicYearId);

        Task<IEnumerable<Sale>> GetProcessed(int academicYearId);
    }
}
