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

namespace MyPortal.Database.Repositories
{
    public class ApplicationPermissionRepository : BaseReadRepository<ApplicationPermission>, IApplicationPermissionRepository
    {
        public ApplicationPermissionRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            RelatedColumns = $@"{EntityHelper.GetAllColumns(typeof(SystemArea))}";
            JoinRelated =
                $@"{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[SystemArea]", "[SystemArea].[Id]", "[AspNetPermissions].[AreaId]")}";
        }

        protected override async Task<IEnumerable<ApplicationPermission>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<ApplicationPermission, SystemArea, ApplicationPermission>(sql,
                (permission, area) =>
                {
                    permission.Area = area;

                    return permission;
                }, param);
        }

        public async Task<ApplicationPermission> GetByClaimValue(int claimValue)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[AspNetPermissions].[ClaimValue] = @ClaimValue");

            return (await ExecuteQuery(sql, new {ClaimValue = claimValue.ToString()})).FirstOrDefault();
        }
    }
}
