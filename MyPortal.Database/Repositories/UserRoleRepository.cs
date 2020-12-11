using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;
using SqlKata.Compilers;

namespace MyPortal.Database.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private SqlServerCompiler _compiler;
        private ApplicationDbContext _context;

        public UserRoleRepository(ApplicationDbContext context)
        {
            _compiler = new SqlServerCompiler();
            _context = context;
        }

        protected void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(User), "User");
            query.SelectAllColumns(typeof(Role), "Role");

            JoinRelated(query);
        }

        protected void JoinRelated(Query query)
        {
            query.LeftJoin("Users as User", "User.Id", "UserRole.UserId");
            query.LeftJoin("Roles as Role", "Role.Id", "UserRole.RoleId");
        }

        private Query GenerateQuery()
        {
            var query = new Query("UserRoles as UserRole").SelectAllColumns(typeof(UserRole), "UserRole");

            SelectAllRelated(query);

            return query;
        }

        protected async Task<IEnumerable<UserRole>> ExecuteQuery(Query query)
        {
            var sql = _compiler.Compile(query);

            return await _context.Database.GetDbConnection().QueryAsync<UserRole, User, Role, UserRole>(sql.Sql, (
                userRole, user, role) =>
                {
                    userRole.User = user;
                    userRole.Role = role;

                    return userRole;
                }, sql.NamedBindings, splitOn:"Id, Id");
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<UserRole>> GetByUser(Guid userId)
        {
            var query = GenerateQuery();

            query.Where("User.Id", userId);

            return await ExecuteQuery(query);
        }
    }
}
