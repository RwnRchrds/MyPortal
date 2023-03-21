using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AttendancePeriodRepository : BaseReadWriteRepository<AttendancePeriod>, IAttendancePeriodRepository
    {
        public AttendancePeriodRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("AttendanceWeekPatterns as AWP", "AWP.Id", $"{TblAlias}.WeekPatternId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AttendanceWeekPattern), "AWP");

            return query;
        }

        protected override async Task<IEnumerable<AttendancePeriod>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var periods =
                await Transaction.Connection.QueryAsync<AttendancePeriod, AttendanceWeekPattern, AttendancePeriod>(
                    sql.Sql,
                    (period, pattern) =>
                    {
                        period.WeekPattern = pattern;

                        return period;
                    }, sql.NamedBindings, Transaction);

            return periods;
        }

        public async Task Update(AttendancePeriod entity)
        {
            var period = await Context.AttendancePeriods.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (period == null)
            {
                throw new EntityNotFoundException("Attendance period not found.");
            }

            period.AmReg = entity.AmReg;
            period.PmReg = entity.PmReg;
            period.StartTime = entity.StartTime;
            period.EndTime = entity.EndTime;
            period.Weekday = entity.Weekday;
            period.Name = entity.Name;
        }

        public async Task<IEnumerable<AttendancePeriodInstance>> GetByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            var query = GenerateEmptyQuery(typeof(AttendancePeriod), "AP");

            query.Select(
                "CONVERT(DATETIME, CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN P.Weekday = 0 THEN 6 ELSE P.Weekday - 1 END, W.Beginning), 112) + ' ' + CONVERT(CHAR(8), P.StartTime, 108)) AS ActualStartTime");

            query.Select(
                "CONVERT(DATETIME, CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN P.Weekday = 0 THEN 6 ELSE P.Weekday - 1 END, W.Beginning), 112) + ' ' + CONVERT(CHAR(8), P.EndTime, 108)) AS ActualEndTime");

            query.Select("AP.Id as PeriodId", "AW.Id as AttendanceWeekId", "P.WeekPatternId as WeekPatternId",
                "P.Weekday as Weekday", "P.Name as Name", "P.StartTime as StartTime", "P.EndTime as EndTime",
                "P.AmReg as AmReg", "P.PmReg as PmReg");

            query.LeftJoin("AttendanceWeekPatterns as AWP", "AWP.Id", $"AP.WeekPatternId");

            query.LeftJoin("AttendanceWeeks AS AW", "AW.WeekPatternId", "AWP.Id");

            query.Where("AW.IsNonTimeTable", false);

            query.Where("ActualStartTime", ">=", dateFrom.Date);
            query.Where("ActualEndTime", "<", dateTo.Date.AddDays(1));

            return await ExecuteQuery<AttendancePeriodInstance>(query);
        }
    }
}