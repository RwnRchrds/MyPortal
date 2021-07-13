using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class SystemAreaRepository : BaseReadRepository<SystemArea>, ISystemAreaRepository
    {
        public SystemAreaRepository(DbTransaction transaction) : base(transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "SystemAreas", "P", "ParentId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(SystemArea), "P");

            return query;
        }

        protected override async Task<IEnumerable<SystemArea>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var systemAreas = await Transaction.Connection.QueryAsync<SystemArea, SystemArea, SystemArea>(sql.Sql,
                (area, parent) =>
                {
                    area.Parent = parent;

                    return area;
                }, sql.NamedBindings, Transaction);

            return systemAreas;
        }
    }
}