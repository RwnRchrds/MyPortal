using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Database.Models.QueryResults.Curriculum;
using MyPortal.Database.Models.Search;

namespace MyPortal.Database.Interfaces.Repositories;

public interface ISessionPeriodRepository : IReadWriteRepository<SessionPeriod>
{
    Task<IEnumerable<SessionPeriodDetailModel>> GetPeriodDetailsBySession(Guid sessionId, DateTime dateFrom,
        DateTime dateTo);

    Task<IEnumerable<SessionPeriodDetailModel>> GetPeriodDetailsBySession(Guid sessionId, Guid attendanceWeekId);

    Task<IEnumerable<SessionPeriodDetailModel>> GetPeriodDetailsByStudent(Guid studentId,
        DateTime dateFrom, DateTime dateTo);

    Task<IEnumerable<SessionPeriodDetailModel>> GetPeriodDetailsByStaffMember(Guid staffMemberId,
        DateTime dateFrom, DateTime dateTo);

    Task<IEnumerable<SessionPeriodDetailModel>> SearchPeriodDetails(RegisterSearchOptions searchOptions);
}