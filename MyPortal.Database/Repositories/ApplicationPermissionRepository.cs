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
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ApplicationPermissionRepository : BaseReadRepository<ApplicationPermission>, IApplicationPermissionRepository
    {
        public ApplicationPermissionRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
           
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(SystemArea));

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.SystemArea", "SystemArea.Id", "AspNetPermissions.AreaId");

            return query;
        }

        protected override async Task<IEnumerable<ApplicationPermission>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<ApplicationPermission, SystemArea, ApplicationPermission>(sql.Sql,
                (permission, area) =>
                {
                    permission.Area = area;

                    return permission;
                }, sql.Bindings);
        }

        public async Task<ApplicationPermission> GetByClaimValue(int claimValue)
        {
            var sql = SelectAllColumns();

            sql.Where("AspNetPermissions.ClaimValue", "=", claimValue);

            return (await ExecuteQuery(sql)).FirstOrDefault();
        }
    }
}
