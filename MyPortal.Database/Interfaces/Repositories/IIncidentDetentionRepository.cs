using System;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IIncidentDetentionRepository : IReadWriteRepository<StudentIncidentDetention>
    {
        Task<StudentIncidentDetention> Get(Guid detentionId, Guid studentId);
    }
}
