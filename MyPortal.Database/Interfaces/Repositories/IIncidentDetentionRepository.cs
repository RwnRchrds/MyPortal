using System;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IIncidentDetentionRepository : IReadWriteRepository<IncidentDetention>
    {
        Task<IncidentDetention> Get(Guid detentionId, Guid studentId);
    }
}
