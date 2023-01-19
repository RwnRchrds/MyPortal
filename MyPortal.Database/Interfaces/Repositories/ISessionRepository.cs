using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Database.Models.Search;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ISessionRepository : IReadWriteRepository<Session>, IUpdateRepository<Session>
    {
        Task<IEnumerable<SessionDetailModel>> GetSessionDetails(Guid sessionId, DateTime dateFrom, DateTime dateTo);
        Task<SessionDetailModel> GetSessionDetails(Guid sessionId, Guid attendanceWeekId);
        Task<IEnumerable<SessionDetailModel>> GetSessionDetailsByStudent(Guid studentId, DateTime dateFrom, DateTime dateTo);
        Task<IEnumerable<SessionDetailModel>> GetSessionDetailsByStaffMember(Guid staffMemberId, DateTime dateFrom,
            DateTime dateTo);

        Task<IEnumerable<SessionDetailModel>> GetSessionDetails(RegisterSearchOptions searchOptions);
    }
}
