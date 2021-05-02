using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;
using SqlKata.Compilers;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private SqlServerCompiler _compiler;
        private ApplicationDbContext _context;
        private DbTransaction _transaction;

        public UserRoleRepository(ApplicationDbContext context, DbTransaction transaction)
        {
            _compiler = new SqlServerCompiler();
            _context = context;
            _transaction = transaction;
        }

        private Query GenerateQuery()
        {
            var query = new Query("UserRoles as UR").SelectAllColumns(typeof(UserRole), "UR");

            return query;
        }

        protected async Task<IEnumerable<UserRole>> ExecuteQuery(Query query)
        {
            var sql = _compiler.Compile(query);

            return await _transaction.Connection.QueryAsync<UserRole, User, Role, UserRole>(sql.Sql, (
                userRole, user, role) =>
                {
                    userRole.User = user;
                    userRole.Role = role;

                    return userRole;
                }, sql.NamedBindings, _transaction, splitOn:"Id, Id");
        }

        public async Task<IEnumerable<UserRole>> GetByUser(Guid userId)
        {
            var query = GenerateQuery();

            query.Where("User.Id", userId);

            return await ExecuteQuery(query);
        }

        public async Task DeleteAllByRole(Guid roleId)
        {
            var roles = await _context.UserRoles.Where(x => x.RoleId == roleId).ToListAsync();

            _context.UserRoles.RemoveRange(roles);
        }

        public async Task DeleteAllByUser(Guid userId)
        {
            var roles = await _context.UserRoles.Where(x => x.UserId == userId).ToListAsync();

            _context.UserRoles.RemoveRange(roles);
        }
    }
}
