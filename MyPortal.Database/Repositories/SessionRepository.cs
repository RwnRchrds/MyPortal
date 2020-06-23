using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class SessionRepository : BaseReadWriteRepository<Session>, ISessionRepository
    {
        public SessionRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Class));
            query.SelectAll(typeof(Period));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Class", "Class.Id", "Session.ClassId");
            query.LeftJoin("dbo.Period", "Period.Id", "Session.PeriodId");
        }

        protected override async Task<IEnumerable<Session>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Session, Class, Period, Session>(sql.Sql, (session, currClass, period) =>
                {
                    session.Class = currClass;
                    session.Period = period;

                    return session;
                }, sql.NamedBindings);
        }
    }
}