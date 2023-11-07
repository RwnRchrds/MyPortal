using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentDetentionRepository : IReadWriteRepository<StudentDetention>
    {
        Task<StudentDetention> GetSpecific(Guid detentionId, Guid studentId);
        Task<IEnumerable<StudentDetention>> GetByStudentIncident(Guid studentIncidentId);
    }
}