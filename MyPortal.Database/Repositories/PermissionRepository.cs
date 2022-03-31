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
    public class PermissionRepository : BaseReadRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(DbTransaction transaction) : base(transaction)
        {
           
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "SystemAreas", "SA", "AreaId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(SystemArea), "SA");

            return query;
        }

        protected override async Task<IEnumerable<Permission>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var permissions = await Transaction.Connection.QueryAsync<Permission, SystemArea, Permission>(sql.Sql,
                (permission, area) =>
                {
                    permission.SystemArea = area;

                    return permission;
                }, sql.NamedBindings, Transaction);

            return permissions;
        }

        public async Task<IEnumerable<Permission>> GetPermissionsByValues(IEnumerable<int> permissionValues)
        {
            var query = GenerateQuery();

            query.WhereIn($"{TblAlias}.Value", permissionValues);

            return await ExecuteQuery(query);
        }
    }
}
