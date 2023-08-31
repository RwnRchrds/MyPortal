using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Constants;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AttendancePeriodRepository : BaseReadWriteRepository<AttendancePeriod>, IAttendancePeriodRepository
    {
        public AttendancePeriodRepository(DbUserWithContext dbUser) : base(dbUser)
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
                await DbUser.Transaction.Connection
                    .QueryAsync<AttendancePeriod, AttendanceWeekPattern, AttendancePeriod>(
                        sql.Sql,
                        (period, pattern) =>
                        {
                            period.WeekPattern = pattern;

                            return period;
                        }, sql.NamedBindings, DbUser.Transaction);

            return periods;
        }

        public async Task Update(AttendancePeriod entity)
        {
            var period = await DbUser.Context.AttendancePeriods.FirstOrDefaultAsync(x => x.Id == entity.Id);

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

        public async Task<IEnumerable<AttendancePeriodInstance>> GetInstancesByDateRange(DateTime dateFrom,
            DateTime dateTo)
        {
            var query = Views.GetAttendancePeriodInstances("API");

            query.Where("API.ActualStartTime", ">=", dateFrom.Date);
            query.Where("API.ActualEndTime", "<", dateTo.Date.AddDays(1));

            return await ExecuteQuery<AttendancePeriodInstance>(query);
        }

        public async Task<AttendancePeriodInstance> GetInstanceByPeriodId(Guid attendanceWeekId, Guid periodId)
        {
            var query = Views.GetAttendancePeriodInstances("API");

            query.Where("API.AttendanceWeekId", attendanceWeekId);
            query.Where("API.PeriodId", periodId);

            return await ExecuteQueryFirstOrDefault<AttendancePeriodInstance>(query);
        }
    }
}