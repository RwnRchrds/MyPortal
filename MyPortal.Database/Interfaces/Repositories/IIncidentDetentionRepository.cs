using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IIncidentDetentionRepository : IReadWriteRepository<StudentIncidentDetention>
    {
        Task<StudentIncidentDetention> GetSpecific(Guid detentionId, Guid studentIncidentId);
        Task<IEnumerable<StudentIncidentDetention>> GetByStudentIncident(Guid studentIncidentId);
    }
}
