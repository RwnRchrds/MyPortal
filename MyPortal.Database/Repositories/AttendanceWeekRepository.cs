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
        public AttendanceWeekRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
      
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("AcademicTerms as AT", "AT.Id", $"{TblAlias}.AcademicTermId");
            query.LeftJoin("AttendanceWeekPatters as AWP", "AWP.Id", $"{TblAlias}.WeekPatternId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AcademicTerm), "AT");
            query.SelectAllColumns(typeof(AttendanceWeekPattern), "AWP");

            return query;
        }

        protected override async Task<IEnumerable<AttendanceWeek>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var weeks = await Transaction.Connection
                .QueryAsync<AttendanceWeek, AcademicTerm, AttendanceWeekPattern, AttendanceWeek>(sql.Sql,
                    (week, term, pattern) =>
                    {
                        week.AcademicTerm = term;
                        week.WeekPattern = pattern;

                        return week;
                    }, sql.NamedBindings, Transaction);

            return weeks;
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