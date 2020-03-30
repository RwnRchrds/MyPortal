using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SessionRepository : BaseReadWriteRepository<Session>, ISessionRepository
    {
        public SessionRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Class))},
{EntityHelper.GetAllColumns(typeof(Period))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Class]", "[Class].[Id]", "[Session].[ClassId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Period]", "[Period].[Id]", "[Session].[PeriodId]")}";
        }

        protected override async Task<IEnumerable<Session>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Session, Class, Period, Session>(sql, (session, currClass, period) =>
                {
                    session.Class = currClass;
                    session.Period = period;

                    return session;
                }, param);
        }
    }
}