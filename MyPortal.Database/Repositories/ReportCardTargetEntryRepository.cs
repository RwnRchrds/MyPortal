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
    public class ReportCardTargetEntryRepository : BaseReadWriteRepository<ReportCardTargetEntry>,
        IReportCardTargetEntryRepository
    {
        public ReportCardTargetEntryRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ReportCardEntries as RCE", "RCE.Id", $"{TblAlias}.EntryId");
            query.LeftJoin("ReportCardTargets as RCT", "RCT.Id", $"{TblAlias}.TargetId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ReportCardEntry), "RCE");
            query.SelectAllColumns(typeof(ReportCardTarget), "RCT");

            return query;
        }

        protected override async Task<IEnumerable<ReportCardTargetEntry>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var entries = await DbUser.Transaction.Connection
                .QueryAsync<ReportCardTargetEntry, ReportCardEntry, ReportCardTarget, ReportCardTargetEntry>(sql.Sql,
                    (entry, cardEntry, target) =>
                    {
                        entry.Entry = cardEntry;
                        entry.Target = target;

                        return entry;
                    }, sql.NamedBindings, DbUser.Transaction);

            return entries;
        }

        public async Task Update(ReportCardTargetEntry entity)
        {
            var entry = await DbUser.Context.ReportCardTargetEntries.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (entry == null)
            {
                throw new EntityNotFoundException("Entry not found.");
            }

            entry.TargetCompleted = entity.TargetCompleted;
        }
    }
}