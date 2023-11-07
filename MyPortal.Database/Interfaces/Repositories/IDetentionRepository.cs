using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDetentionRepository : IReadWriteRepository<Detention>, IUpdateRepository<Detention>
    {
        Task<IEnumerable<Detention>> GetByStudent(Guid studentId, DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<Detention>> GetByStudent(Guid studentId, Guid academicYearId);

        Task<Detention> GetByIncident(Guid incidentId);

        Task<IEnumerable<Detention>> GetAll(DetentionSearchOptions searchOptions);
    }
}