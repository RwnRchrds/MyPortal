using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IIncidentRepository : IReadWriteRepository<Incident>
    {
        Task<IEnumerable<Incident>> GetByStudent(Guid studentId, Guid academicYearId);

        Task<int> GetCountByStudent(Guid studentId, Guid academicYearId);

        Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId);
    }
}
