using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ReportCardEntryRepository : BaseReadWriteRepository<ReportCardEntry>, IReportCardEntryRepository
    {
        public ReportCardEntryRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ReportCards", "RC", "ReportCardId");
            JoinEntity(query, "Users", "U", "CreatedById");
            JoinEntity(query, "AttendanceWeeks", "AW", "WeekId");
            JoinEntity(query, "AttendancePeriods", "AP", "PeriodId");

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

            var entries = await Transaction.Connection
                .QueryAsync<ReportCardEntry, ReportCard, User, AttendanceWeek, AttendancePeriod, ReportCardEntry>(
                    sql.Sql,
                    (entry, card, user, week, period) =>
                    {
                        entry.ReportCard = card;
                        entry.CreatedBy = user;
                        entry.AttendanceWeek = week;
                        entry.Period = period;

                        return entry;
                    }, sql.NamedBindings, Transaction);

            return entries;
        }

        public async Task Update(ReportCardEntry entity)
        {
            var entry = await Context.ReportCardEntries.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entry == null)
            {
                throw new EntityNotFoundException("Entry not found.");
            }

            entry.Comments = entity.Comments;
        }
    }
}