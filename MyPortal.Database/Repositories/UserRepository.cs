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
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class UserRepository : BaseReadWriteRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context, "User")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "Person");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("People as Person", "Person.Id", "User.PersonId");
        }

        protected override async Task<IEnumerable<User>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<User, Person, User>(sql.Sql, (user, person) =>
            {
                user.Person = person;

                return user;
            }, sql.NamedBindings);
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