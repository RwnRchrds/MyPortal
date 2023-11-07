using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class UserRepository : BaseReadWriteRepository<User>, IUserRepository
    {
        public UserRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "P");

            return query;
        }

        protected override async Task<IEnumerable<User>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var users = await DbUser.Transaction.Connection.QueryAsync<User, Person, User>(sql.Sql, (user, person) =>
            {
                user.Person = person;

                return user;
            }, sql.NamedBindings, DbUser.Transaction);

            return users;
        }

        public async Task<bool> UserExists(string username)
        {
            var query = GetEmptyQuery(typeof(User), "User");

            query.Where($"{TblAlias}.UserName", username);

            query.AsCount();

            var result = await ExecuteQueryIntResult(query);

            return result > 0;
        }

        public async Task<User> GetByUsername(string username)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.UserName", username);

            return await ExecuteQueryFirstOrDefault(query);
        }

        public async Task Update(User entity)
        {
            var user = await DbUser.Context.Users.FirstOrDefaultAsync(x => x.Id == entity.Id);

            user.AccessFailedCount = entity.AccessFailedCount;
            user.Email = entity.Email;
            user.EmailConfirmed = entity.EmailConfirmed;
            user.LockoutEnabled = entity.LockoutEnabled;
            user.LockoutEnd = entity.LockoutEnd;
            user.PhoneNumber = entity.PhoneNumber;
            user.PhoneNumberConfirmed = entity.PhoneNumberConfirmed;
            user.CreatedDate = entity.CreatedDate;
            user.PersonId = entity.PersonId;
            user.UserType = entity.UserType;
            user.Enabled = entity.Enabled;
        }
    }
}