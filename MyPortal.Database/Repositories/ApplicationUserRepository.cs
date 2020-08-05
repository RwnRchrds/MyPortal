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
    public class ApplicationUserRepository : BaseReadRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDbConnection connection) : base(connection, "User")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "Person");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("People as Person", "Person.UserId", "User.Id");
        }

        protected override async Task<IEnumerable<ApplicationUser>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<ApplicationUser, Person, ApplicationUser>(sql.Sql, (user, person) =>
            {
                user.Person = person;

                return user;
            }, sql.NamedBindings);
        }
    }
}