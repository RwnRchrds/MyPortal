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
    public class PeriodRepository : BaseReadWriteRepository<Period>, IPeriodRepository
    {
        public PeriodRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(AttendanceWeekPattern), "WeekPattern");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.AttendanceWeekPattern as WeekPattern", "WeekPattern.Id", "Period.WeekPatternId");
        }

        protected override async Task<IEnumerable<Period>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Period, AttendanceWeekPattern, Period>(sql.Sql, (period, pattern) =>
            {
                period.WeekPattern = pattern;

                return period;
            }, sql.NamedBindings);
        }
    }
}