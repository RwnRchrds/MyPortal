using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;
using SqlKata.Compilers;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private SqlServerCompiler _compiler;
        private ApplicationDbContext _context;


        public RolePermissionRepository(ApplicationDbContext context)
        {
           _compiler = new SqlServerCompiler();
           _context = context;
        }

        protected void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Role), "Role");
            query.SelectAllColumns(typeof(Permission), "Permission");

            JoinRelated(query);
        }

        protected void JoinRelated(Query query)
        {
            query.LeftJoin("AspNetRoles AS Role", "Role.Id", "RolePermissions.RoleId");
            query.LeftJoin("Permissions AS Permission", "Permission.Id",
                "RolePermissions.PermissionId");
        }

        private Query GenerateQuery()
        {
            var query = new Query("AspNetRolePermissions as RolePermissions").SelectAllColumns(typeof(RolePermission), "RolePermissions");
            
            SelectAllRelated(query);

            return query;
        }

        protected async Task<IEnumerable<RolePermission>> ExecuteQuery(Query query)
        {
            var sql = _compiler.Compile(query);

            return await _context.Database.GetDbConnection()
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

        public void Create(RolePermission rolePermission)
        {
            _context.RolePermissions.Add(rolePermission);
        }

        public async Task Delete(Guid roleId, Guid permissionId)
        {
            var permission =
                await _context.RolePermissions.FirstOrDefaultAsync(x =>
                    x.RoleId == roleId && x.PermissionId == permissionId);

            if (permission != null)
            {
                _context.RolePermissions.Remove(permission);
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RolePermission>> GetByUser(Guid userId)
        {
            var query = GenerateQuery();

            query.Join("AspNetUserRoles AS UserRole", "UserRole.RoleId", "Role.Id");
            query.Join("AspNetUsers AS User", "User.Id", "UserRole.UserId");

            query.Where("User.Id", userId);

            return await ExecuteQuery(query);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}