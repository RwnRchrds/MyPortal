using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SessionRepository : BaseReadWriteRepository<Session>, ISessionRepository
    {
        public SessionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task<IEnumerable<SessionDetailModel>> GetSessionDetails(Guid sessionId, DateTime dateFrom, DateTime dateTo)
        {
            var query = Views.GetSessionMetadata("SM");

            query.Where("SM.SessionId", sessionId);
            query.WhereDate("SM.StartTime", ">=", dateFrom);
            query.WhereDate("SM.EndTime", "<=", dateTo);

            return await ExecuteQuery<SessionDetailModel>(query);
        }

        public async Task<SessionDetailModel> GetSessionDetails(Guid sessionId, Guid attendanceWeekId)
        {
            var query = Views.GetSessionMetadata("SM");

            query.Where("SM.SessionId", sessionId);
            query.Where("SM.AttendanceWeekId", attendanceWeekId);

            return await ExecuteQueryFirstOrDefault<SessionDetailModel>(query);
        }

        public async Task<IEnumerable<SessionDetailModel>> GetSessionDetails(RegisterSearchOptions searchOptions)
        {
            var query = Views.GetSessionMetadata("SM");
            
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

            return await ExecuteQuery<SessionDetailModel>(query);
        }

        public async Task<IEnumerable<SessionDetailModel>> GetSessionDetailsByStudent(Guid studentId, DateTime dateFrom, DateTime dateTo)
        {
            var query = Views.GetSessionMetadata("SM");
            
            query.LeftJoin("StudentGroupMemberships AS SGM", "SGM.StudentGroupId", "SM.StudentGroupId");
            query.LeftJoin("Students AS S", "S.Id", "SGM.StudentId");

            query.Where("S.Id", studentId);
            query.Where("SM.StartTime", ">=", dateFrom);
            query.Where("SM.EndTime", "<=", dateTo);
            query.WhereStudentGroupMembershipValid("SGM", dateFrom, dateTo);

            return await ExecuteQuery<SessionDetailModel>(query);
        }

        public async Task<IEnumerable<SessionDetailModel>> GetSessionDetailsByStaffMember(Guid staffMemberId, DateTime dateFrom, DateTime dateTo)
        {
            var query = Views.GetSessionMetadata("SM");

            query.LeftJoin("StaffMembers AS S", "S.Id", "SM.TeacherId");
            
            query.Where("S.Id", staffMemberId);
            query.WhereDate("SM.StartTime", ">=", dateFrom);
            query.WhereDate("SM.EndTime", "<=", dateTo);
            
            return await ExecuteQuery<SessionDetailModel>(query);
        }

        public async Task Update(Session entity)
        {
            var session = await Context.Sessions.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (session == null)
            {
                throw new EntityNotFoundException("Session not found.");
            }

            session.PeriodId = entity.PeriodId;
            session.RoomId = entity.RoomId;
            session.TeacherId = entity.TeacherId;
        }
    }
}