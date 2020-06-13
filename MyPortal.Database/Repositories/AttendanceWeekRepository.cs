using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AttendanceWeekRepository : BaseReadWriteRepository<AttendanceWeek>, IAttendanceWeekRepository
    {
        public AttendanceWeekRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
      
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(AcademicYear));

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.AcademicYear", "AcademicYear.Id", "AttendanceWeek.AcademicYearId");

            return query;
        }

        protected override async Task<IEnumerable<AttendanceWeek>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<AttendanceWeek, AcademicYear, AttendanceWeek>(sql.Sql, (week, year) =>
            {
                week.AcademicYear = year;

                return week;
            }, sql.Bindings);
        }
    }
}