using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore.Internal;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class PermissionRepository : BaseReadRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IDbConnection connection) : base(connection)
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

            return await Connection.QueryAsync<Permission, SystemArea, Permission>(sql.Sql,
                (permission, area) =>
                {
                    permission.SystemArea = area;

                    return permission;
                }, sql.NamedBindings);
        }
    }
}
