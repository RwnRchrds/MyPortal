using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ParentEveningGroupRepository : BaseReadWriteRepository<ParentEveningGroup>, IParentEveningGroupRepository
    {
        public ParentEveningGroupRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "ParentEvenings", "PE", "ParentEveningId");
            JoinEntity(query, "StudentGroups", "SG", "StudentGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ParentEvening), "PE");
            query.SelectAllColumns(typeof(StudentGroup), "SG");

            return query;
        }

        protected override async Task<IEnumerable<ParentEveningGroup>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var groups = await Transaction.Connection
                .QueryAsync<ParentEveningGroup, ParentEvening, StudentGroup, ParentEveningGroup>(sql.Sql,
                    (eg, evening, group) =>
                    {
                        eg.ParentEvening = evening;
                        eg.StudentGroup = group;

                        return eg;
                    }, sql.NamedBindings, Transaction);

            return groups;
        }
    }
}