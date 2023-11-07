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
    public class ParentEveningGroupRepository : BaseReadWriteRepository<ParentEveningGroup>,
        IParentEveningGroupRepository
    {
        public ParentEveningGroupRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ParentEvenings as PE", "PE.Id", $"{TblAlias}.ParentEveningId");
            query.LeftJoin("StudentGroups as SG", "SG.Id", $"{TblAlias}.StudentGroupId");

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

            var groups = await DbUser.Transaction.Connection
                .QueryAsync<ParentEveningGroup, ParentEvening, StudentGroup, ParentEveningGroup>(sql.Sql,
                    (eg, evening, group) =>
                    {
                        eg.ParentEvening = evening;
                        eg.StudentGroup = group;

                        return eg;
                    }, sql.NamedBindings, DbUser.Transaction);

            return groups;
        }
    }
}