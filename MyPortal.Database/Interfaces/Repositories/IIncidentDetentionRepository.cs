using System;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IIncidentDetentionRepository : IReadWriteRepository<StudentIncidentDetention>
    {
        Task<StudentIncidentDetention> GetByStudentIncident(Guid detentionId, Guid studentIncidentId);
    }
}
