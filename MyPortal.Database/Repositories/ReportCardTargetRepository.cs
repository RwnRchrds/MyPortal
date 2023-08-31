using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ReportCardTargetRepository : BaseReadWriteRepository<ReportCardTarget>, IReportCardTargetRepository
    {
        public ReportCardTargetRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ReportCards as RC", "RC.Id", $"{TblAlias}.ReportCardId");
            query.LeftJoin("BehaviourTargets as BT", "BT.Id", $"{TblAlias}.TargetId");

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

            var targets = await DbUser.Transaction.Connection
                .QueryAsync<ReportCardTarget, ReportCard, BehaviourTarget, ReportCardTarget>(sql.Sql,
                    (rct, card, target) =>
                    {
                        rct.ReportCard = card;
                        rct.Target = target;

                        return rct;
                    }, sql.NamedBindings, DbUser.Transaction);

            return targets;
        }
    }
}