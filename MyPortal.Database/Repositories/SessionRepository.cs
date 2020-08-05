using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class SessionRepository : BaseReadWriteRepository<Session>, ISessionRepository
    {
        public SessionRepository(ApplicationDbContext context) : base(context, "Session")
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

            return await Connection.QueryAsync<Session, Class, AttendancePeriod, Session>(sql.Sql, (session, currClass, period) =>
                {
                    session.Class = currClass;
                    session.AttendancePeriod = period;

                    return session;
                }, sql.NamedBindings);
        }
    }
}