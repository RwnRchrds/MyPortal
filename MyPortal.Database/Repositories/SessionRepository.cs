using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Query.Attendance;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class SessionRepository : BaseReadWriteRepository<Session>, ISessionRepository
    {
        public SessionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "Session")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Class), "Class");
            query.SelectAllColumns(typeof(AttendancePeriod), "Period");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Classes as Class", "Class.Id", "Session.ClassId");
            query.LeftJoin("AttendancePeriods as Period", "Period.Id", "Session.PeriodId");
        }



        protected override async Task<IEnumerable<Session>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<Session, Class, AttendancePeriod, Session>(sql.Sql, (session, currClass, period) =>
                {
                    session.Class = currClass;
                    session.AttendancePeriod = period;

                    return session;
                }, sql.NamedBindings);
        }

        public async Task<IEnumerable<SessionMetadata>> GetMetadata(Guid sessionId, DateTime dateFrom, DateTime dateTo)
        {
            var query = new Query("Sessions_Metadata AS SM");

            query.Where("SM.SessionId", sessionId);
            query.WhereDate("SM.StartTime", ">=", dateFrom);
            query.WhereDate("SM.EndTime", "<=", dateTo);

            return await ExecuteQuery<SessionMetadata>(query);
        }

        public async Task<SessionMetadata> GetMetadata(Guid sessionId, Guid attendanceWeekId)
        {
            var query = new Query("Sessions_Metadata AS SM");

            query.Where("SM.SessionId", sessionId);
            query.Where("SM.AttendanceWeekId", attendanceWeekId);

            return await ExecuteQueryFirstOrDefault<SessionMetadata>(query);
        }

        public async Task<IEnumerable<SessionMetadata>> GetMetadataByStudent(Guid studentId, DateTime dateFrom, DateTime dateTo)
        {
            var query = new Query("Sessions_Metadata AS SM");

            query.LeftJoin("CurriculumGroupMemberships AS CGM", "CGM.GroupId", "SM.CurriculumGroupId");
            query.LeftJoin("Students AS S", "S.Id", "CGM.StudentId");

            query.Where("S.Id", studentId);
            query.WhereDate("SM.StartTime", ">=", dateFrom);
            query.WhereDate("SM.EndTime", "<=", dateTo);

            return await ExecuteQuery<SessionMetadata>(query);
        }

        public async Task<IEnumerable<SessionMetadata>> GetMetadataByStaffMember(Guid staffMember, DateTime dateFrom, DateTime dateTo)
        {
            return null;
        }
    }
}