using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Repositories
{
    public class ApplicationRolePermissionRepository : BaseReadWriteRepository<ApplicationRolePermission>, IApplicationRolePermissionRepository
    {
        public ApplicationRolePermissionRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(ApplicationRole), "Role")},
{EntityHelper.GetAllColumns(typeof(ApplicationPermission), "Permission")}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetRoles]", "[Role].[Id]", "[ApplicationRolePermission].[RoleId]", "Role")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetPermissions]", "[Permission].[Id]", "[ApplicationRolePermission].[PermissionId]", "Permission")}";
        }

        protected override async Task<IEnumerable<ApplicationRolePermission>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<ApplicationRolePermission, ApplicationRole, ApplicationPermission, ApplicationRolePermission
                >(sql,
                    (rp, role, perm) =>
                    {
                        rp.Role = role;
                        rp.Permission = perm;

                        return rp;
                    }, param);
        }

        public async Task<IEnumerable<ApplicationRolePermission>> GetPermissionsByRole(Guid roleId)
        {
            var sql = SelectAllColumns();
            
            SqlHelper.Where(ref sql, "[ApplicationRolePermission].[RoleId] = @RoleId");

            return await ExecuteQuery(sql, new {RoleId = roleId});
        }

        public async Task<IEnumerable<string>> GetClaimValuesByRole(Guid roleId)
        {
            var sql = $"SELECT [Permission].[ClaimValue] FROM {TblName} {JoinRelated}";
            
            SqlHelper.Where(ref sql, "[ApplicationRolePermission].[RoleId] = @RoleId");

            return await Connection.QueryAsync<string>(sql, new {RoleId = roleId});
        }
    }
}