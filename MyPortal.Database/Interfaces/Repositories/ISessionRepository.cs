using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Query.Attendance;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ISessionRepository : IReadWriteRepository<Session>
    {
        Task<IEnumerable<SessionMetadata>> GetMetadata(Guid sessionId, DateTime dateFrom, DateTime dateTo);
        Task<SessionMetadata> GetMetadata(Guid sessionId, Guid attendanceWeekId);
        Task<IEnumerable<SessionMetadata>> GetMetadataByStudent(Guid studentId, DateTime dateFrom, DateTime dateTo);
        Task<IEnumerable<SessionMetadata>> GetMetadataByStaffMember(Guid staffMember, DateTime dateFrom,
            DateTime dateTo);
    }
}
