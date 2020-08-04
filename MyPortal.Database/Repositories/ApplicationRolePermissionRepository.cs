using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ApplicationRolePermissionRepository : BaseReadWriteRepository<ApplicationRolePermission>, IApplicationRolePermissionRepository
    {
        public ApplicationRolePermissionRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(ApplicationRole), "Role");
            query.SelectAll(typeof(ApplicationPermission), "Permission");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AspNetRoles AS Role", "Role.Id", "AspNetRolePermissions.RoleId");
            query.LeftJoin("AspNetPermissions AS Permission", "Permission.Id",
                "AspNetRolePermissions.PermissionId");
        }

        protected override async Task<IEnumerable<ApplicationRolePermission>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<ApplicationRolePermission, ApplicationRole, ApplicationPermission, ApplicationRolePermission
                >(sql.Sql,
                    (rp, role, perm) =>
                    {
                        rp.Role = role;
                        rp.Permission = perm;

                        return rp;
                    }, sql.NamedBindings);
        }

        public async Task<IEnumerable<ApplicationRolePermission>> GetByRole(Guid roleId)
        {
            var query = GenerateQuery();
            
            query.Where("AspNetRolePermissions.RoleId", "=", roleId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<string>> GetClaimValuesByRole(Guid roleId)
        {
            var query = new Query(TblName).Select("Permission.ClaimValue");

            JoinRelated(query);
            
            query.Where("AspNetRolePermissions.RoleId", "=", roleId);

            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<string>(sql.Sql, sql.NamedBindings);
        }
    }
}