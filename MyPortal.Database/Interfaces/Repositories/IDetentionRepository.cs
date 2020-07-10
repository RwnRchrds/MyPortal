using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDetentionRepository : IReadWriteRepository<Detention>
    {
        Task<IEnumerable<Detention>> GetByStudent(Guid studentId, Tuple<DateTime, DateTime> dateRange);

        Task<IEnumerable<Detention>> GetByStudent(Guid studentId, Guid academicYearId);
    }
}
