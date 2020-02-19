using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Repositories
{
    public class ApplicationUserRepository : BaseReadRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDbConnection connection) : base(connection, "User")
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Person))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[UserId]", "[User].[Id]")}";
        }

        protected override async Task<IEnumerable<ApplicationUser>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<ApplicationUser, Person, ApplicationUser>(sql, (user, person) =>
            {
                user.Person = person;

                return user;
            }, param);
        }
    }
}