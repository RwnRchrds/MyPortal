using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ReportCardTargetRepository : BaseReadWriteRepository<ReportCardTarget>, IReportCardTargetRepository
    {
        public ReportCardTargetRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ReportCards", "RC", "ReportCardId");
            JoinEntity(query, "BehaviourTargets", "BT", "TargetId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ReportCard), "RC");
            query.SelectAllColumns(typeof(BehaviourTarget), "BT");

            return query;
        }

        protected override async Task<IEnumerable<ReportCardTarget>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var targets = await Transaction.Connection
                .QueryAsync<ReportCardTarget, ReportCard, BehaviourTarget, ReportCardTarget>(sql.Sql,
                    (rct, card, target) =>
                    {
                        rct.ReportCard = card;
                        rct.Target = target;

                        return rct;
                    }, sql.NamedBindings, Transaction);

            return targets;
        }
    }
}