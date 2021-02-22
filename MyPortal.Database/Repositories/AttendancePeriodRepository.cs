using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AttendancePeriodRepository : BaseReadWriteRepository<AttendancePeriod>, IAttendancePeriodRepository
    {
        public AttendancePeriodRepository(ApplicationDbContext context) : base(context, "AttendancePeriod")
        {
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AttendanceWeekPattern), "WeekPattern");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AttendanceWeekPatterns as WeekPattern", "WeekPattern.Id", "AttendancePeriod.WeekPatternId");
        }

        protected override async Task<IEnumerable<AttendancePeriod>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<AttendancePeriod, AttendanceWeekPattern, AttendancePeriod>(sql.Sql, (period, pattern) =>
            {
                period.WeekPattern = pattern;

                return period;
            }, sql.NamedBindings);
        }
    }
}