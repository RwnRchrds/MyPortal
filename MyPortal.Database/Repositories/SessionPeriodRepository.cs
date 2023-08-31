using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Database.Models.QueryResults.Curriculum;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories;

public class SessionPeriodRepository : BaseReadWriteRepository<SessionPeriod>, ISessionPeriodRepository
{
    public SessionPeriodRepository(DbUserWithContext dbUser) : base(dbUser)
    {
    }

    public async Task<IEnumerable<SessionPeriodDetailModel>> GetPeriodDetailsBySession(Guid sessionId, DateTime dateFrom, DateTime dateTo)
    {
        var query = Views.GetSessionPeriodMetadata("SM");

        query.Where("SM.SessionId", sessionId);
        query.WhereDate("SM.StartTime", ">=", dateFrom);
        query.WhereDate("SM.EndTime", "<=", dateTo);

        return await ExecuteQuery<SessionPeriodDetailModel>(query);
    }
    
    public async Task<IEnumerable<SessionPeriodDetailModel>> GetPeriodDetailsBySession(Guid sessionId, Guid attendanceWeekId)
    {
        var query = Views.GetSessionPeriodMetadata("SM");

        query.Where("SM.SessionId", sessionId);
        query.Where("SM.AttendanceWeekId", attendanceWeekId);

        return await ExecuteQuery<SessionPeriodDetailModel>(query);
    }

    public async Task<IEnumerable<SessionPeriodDetailModel>> GetPeriodDetailsByStudent(Guid studentId,
        DateTime dateFrom, DateTime dateTo)
    {
        var query = Views.GetSessionPeriodMetadata("SM");
            
        query.LeftJoin("StudentGroupMemberships AS SGM", "SGM.StudentGroupId", "SM.StudentGroupId");
        query.LeftJoin("Students AS S", "S.Id", "SGM.StudentId");

        query.Where("S.Id", studentId);
        query.Where("SM.StartTime", ">=", dateFrom);
        query.Where("SM.EndTime", "<=", dateTo);
        query.WhereStudentGroupMembershipValid("SGM", dateFrom, dateTo);

        return await ExecuteQuery<SessionPeriodDetailModel>(query);
    }

    public async Task<IEnumerable<SessionPeriodDetailModel>> GetPeriodDetailsByStaffMember(Guid staffMemberId,
        DateTime dateFrom, DateTime dateTo)
    {
        var query = Views.GetSessionPeriodMetadata("SM");

        query.LeftJoin("StaffMembers AS S", "S.Id", "SM.TeacherId");
            
        query.Where("S.Id", staffMemberId);
        query.WhereDate("SM.StartTime", ">=", dateFrom);
        query.WhereDate("SM.EndTime", "<=", dateTo);
            
        return await ExecuteQuery<SessionPeriodDetailModel>(query);
    }

    public async Task<IEnumerable<SessionPeriodDetailModel>> SearchPeriodDetails(RegisterSearchOptions searchOptions)
    {
        var query = Views.GetSessionPeriodMetadata("SM");
            
        query.WhereDate("SM.StartTime", ">=", searchOptions.DateFrom);
        query.WhereDate("SM.EndTime", "<=", searchOptions.DateTo);

        if (searchOptions.PeriodId.HasValue)
        {
            query.Where("SM.PeriodId", searchOptions.PeriodId);
        }

        if (searchOptions.TeacherId.HasValue)
        {
            query.Where("SM.TeacherId", searchOptions.TeacherId);
        }

        return await ExecuteQuery<SessionPeriodDetailModel>(query);
    }
}