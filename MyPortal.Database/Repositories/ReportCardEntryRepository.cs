using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ReportCardEntryRepository : BaseReadWriteRepository<ReportCardEntry>, IReportCardEntryRepository
    {
        public ReportCardEntryRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ReportCards as RC", "RC.Id", $"{TblAlias}.ReportCardId");
            query.LeftJoin("Users as U", "U.Id", $"{TblAlias}.CreatedById");
            query.LeftJoin("AttendanceWeeks as AW", "AW.Id", $"{TblAlias}.WeekId");
            query.LeftJoin("AttendancePeriods as AP", "AP.Id", $"{TblAlias}.PeriodId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ReportCard), "RC");
            query.SelectAllColumns(typeof(User), "U");
            query.SelectAllColumns(typeof(AttendanceWeek), "AW");
            query.SelectAllColumns(typeof(AttendancePeriod), "AP");

            return query;
        }

        protected override async Task<IEnumerable<ReportCardEntry>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var entries = await DbUser.Transaction.Connection
                .QueryAsync<ReportCardEntry, ReportCard, User, AttendanceWeek, AttendancePeriod, ReportCardEntry>(
                    sql.Sql,
                    (entry, card, user, week, period) =>
                    {
                        entry.ReportCard = card;
                        entry.CreatedBy = user;
                        entry.AttendanceWeek = week;
                        entry.Period = period;

                        return entry;
                    }, sql.NamedBindings, DbUser.Transaction);

            return entries;
        }

        public async Task Update(ReportCardEntry entity)
        {
            var entry = await DbUser.Context.ReportCardEntries.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entry == null)
            {
                throw new EntityNotFoundException("Entry not found.");
            }

            entry.Comments = entity.Comments;
        }
    }
}