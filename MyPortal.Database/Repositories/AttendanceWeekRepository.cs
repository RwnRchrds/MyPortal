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
        public AttendanceWeekRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
      
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(AcademicYear));
            query.SelectAll(typeof(AttendanceWeekPattern), "WeekPattern");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.AcademicYear", "AcademicYear.Id", "AttendanceWeek.AcademicYearId");
            query.LeftJoin("dbo.AttendanceWeekPattern as WeekPattern", "WeekPattern.Id",
                "AttendanceWeek.WeekPatternId");
        }

        protected override async Task<IEnumerable<AttendanceWeek>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<AttendanceWeek, AcademicYear, AttendanceWeek>(sql.Sql, (week, year) =>
            {
                week.AcademicYear = year;

                return week;
            }, sql.NamedBindings);
        }
    }
}