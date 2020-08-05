using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AttendanceWeekRepository : BaseReadWriteRepository<AttendanceWeek>, IAttendanceWeekRepository
    {
        public AttendanceWeekRepository(ApplicationDbContext context) : base(context, "AttendanceWeek")
        {
      
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AttendanceWeekPattern), "WeekPattern");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AttendanceWeekPatterns as WeekPattern", "WeekPattern.Id",
                "AttendanceWeek.WeekPatternId");
        }

        protected override async Task<IEnumerable<AttendanceWeek>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<AttendanceWeek, AttendanceWeekPattern, AttendanceWeek>(sql.Sql, (week, pattern) =>
            {
                week.WeekPattern = pattern;

                return week;
            }, sql.NamedBindings);
        }

        public async Task<AttendanceWeek> GetByDate(DateTime date)
        {
            var query = GenerateQuery();

            query.WhereDate("AttendanceWeek.Beginning", "<=", date);
            query.WhereDate("DATEADD(DAY, 6, AttendanceWeek.Beginning)", ">=", date);

            return await ExecuteQueryFirstOrDefault(query);
        }
    }
}