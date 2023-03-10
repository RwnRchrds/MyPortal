using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class UserRepository : BaseReadWriteRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "People", "P", "PersonId");

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

            var users = await Transaction.Connection.QueryAsync<User, Person, User>(sql.Sql, (user, person) =>
            {
                user.Person = person;

                return user;
            }, sql.NamedBindings, Transaction);

            return users;
        }

        public async Task<bool> UserExists(string username)
        {
            var query = GenerateEmptyQuery(typeof(User), "User");

            query.Where("User.UserName", username);

            query.AsCount();

            var result = await ExecuteQueryIntResult(query);

            return result > 0;
        }

        public async Task<User> GetByUsername(string username)
        {
            var query = GenerateQuery();

            query.Where("User.UserName", username);

            return await ExecuteQueryFirstOrDefault(query);
        }
    }
}