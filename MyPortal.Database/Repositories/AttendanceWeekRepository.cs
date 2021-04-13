using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AttendanceWeekRepository : BaseReadWriteRepository<AttendanceWeek>, IAttendanceWeekRepository
    {
        public AttendanceWeekRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "AttendanceWeek")
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

            return await Transaction.Connection.QueryAsync<AttendanceWeek, AttendanceWeekPattern, AttendanceWeek>(sql.Sql, (week, pattern) =>
            {
                week.WeekPattern = pattern;

                return week;
            }, sql.NamedBindings, Transaction);
        }

        public async Task<AttendanceWeek> GetByDate(DateTime date)
        {
            var query = GenerateQuery();

            query.WhereDate("AttendanceWeek.Beginning", "<=", date);
            query.WhereDate("DATEADD(DAY, 6, AttendanceWeek.Beginning)", ">=", date);

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task<IEnumerable<AttendanceWeek>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var query = GenerateQuery();

            query.WhereDate("AttendanceWeek.Beginning", "<=", startDate);
            query.WhereDate("DATEADD(DAY, 6, AttendanceWeek.Beginning)", ">=", endDate);

            return await ExecuteQuery(query);
        }


        public async Task Update(AttendanceWeek entity)
        {
            var attendanceWeek = await Context.AttendanceWeeks.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (attendanceWeek == null)
            {
                throw new EntityNotFoundException("Attendance week not found.");
            }

            attendanceWeek.IsNonTimetable = entity.IsNonTimetable;
        }
    }
}