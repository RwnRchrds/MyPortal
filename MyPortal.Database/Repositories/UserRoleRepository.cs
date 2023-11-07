using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using SqlKata;
using SqlKata.Compilers;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private SqlServerCompiler _compiler;
        private DbUserWithContext _dbUser;

        public UserRoleRepository(DbUserWithContext dbUser)
        {
            _compiler = new SqlServerCompiler();
            _dbUser = dbUser;
        }

        private Query GenerateQuery()
        {
            var query = new Query("UserRoles as UR").SelectAllColumns(typeof(UserRole), "UR");

            query.LeftJoin("Users as U", "U.Id", "UR.UserId");
            query.LeftJoin("Roles as R", "R.Id", "UR.RoleId");
            query.SelectAllColumns(typeof(User), "U");
            query.SelectAllColumns(typeof(Role), "R");

            return query;
        }

        protected async Task<IEnumerable<UserRole>> ExecuteQuery(Query query)
        {
            var sql = _compiler.Compile(query);

            return await _dbUser.Transaction.Connection.QueryAsync<UserRole, User, Role, UserRole>(sql.Sql, (
                userRole, user, role) =>
            {
                userRole.User = user;
                userRole.Role = role;

                return userRole;
            }, sql.NamedBindings, _dbUser.Transaction, splitOn: "Id, Id");
        }

        public async Task<IEnumerable<UserRole>> GetByUser(Guid userId)
        {
            var query = GenerateQuery();

            query.Where("U.Id", userId);

            return await ExecuteQuery(query);
        }

        public async Task DeleteAllByRole(Guid roleId)
        {
            var roles = await _dbUser.Context.UserRoles.Where(x => x.RoleId == roleId).ToListAsync();

            _dbUser.Context.UserRoles.RemoveRange(roles);
        }

        public async Task DeleteAllByUser(Guid userId)
        {
            var roles = await _dbUser.Context.UserRoles.Where(x => x.UserId == userId).ToListAsync();

            _dbUser.Context.UserRoles.RemoveRange(roles);
        }
    }
}