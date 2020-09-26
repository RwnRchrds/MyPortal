using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class RolePermissionRepository : BaseReadWriteRepository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(ApplicationDbContext context) : base(context, "RolePermissions")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Role), "Role");
            query.SelectAllColumns(typeof(Permission), "Permission");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AspNetRoles AS Role", "Role.Id", "RolePermissions.RoleId");
            query.LeftJoin("Permissions AS Permission", "Permission.Id",
                "RolePermissions.PermissionId");
        }

        protected override async Task<IEnumerable<RolePermission>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<RolePermission, Role, Permission, RolePermission
                >(sql.Sql,
                    (rp, role, perm) =>
                    {
                        rp.Role = role;
                        rp.Permission = perm;

                        return rp;
                    }, sql.NamedBindings, splitOn:"Id,Id");
        }

        public async Task<IEnumerable<RolePermission>> GetByRole(Guid roleId)
        {
            var query = GenerateQuery();
            
            query.Where("RolePermissions.RoleId", "=", roleId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<RolePermission>> GetByUser(Guid userId)
        {
            var query = GenerateQuery();

            query.Join("AspNetUserRoles AS UserRole", "UserRole.RoleId", "Role.Id");
            query.Join("AspNetUsers AS User", "User.Id", "UserRole.UserId");

            query.Where("User.Id", userId);

            return await ExecuteQuery(query);
        }
    }
}