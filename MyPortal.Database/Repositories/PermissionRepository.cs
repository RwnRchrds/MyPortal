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

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(SystemArea), "SystemArea");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("SystemAreas as SystemArea", "SystemArea.Id", "Permissions.AreaId");
        }

        protected override async Task<IEnumerable<Permission>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<Permission, SystemArea, Permission>(sql.Sql,
                (permission, area) =>
                {
                    permission.SystemArea = area;

                    return permission;
                }, sql.NamedBindings, Transaction);
        }
    }
}
