using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Repositories
{
    public class PersonRepository : BaseReadWriteRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
       RelatedColumns = $"{EntityHelper.GetUserProperties("User")}";

        JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[Person].[UserId]", "User")}";
        }

        public async Task<Person> GetByUserId(Guid userId)
        {
            var sql = SelectAllColumns();

            QueryHelper.Where(ref sql, "[Person].[UserId] = @UserId");

            return (await ExecuteQuery(sql, new {UserId = userId})).FirstOrDefault();
        }

        private static void ApplySearch(ref string sql, Person person)
        {
            if (!string.IsNullOrWhiteSpace(person.FirstName))
            {
                QueryHelper.Where(ref sql, "[Person].[FirstName] LIKE @FirstName");
            }

            if (!string.IsNullOrWhiteSpace(person.LastName))
            {
                QueryHelper.Where(ref sql, "[Person].[LastName] LIKE @LastName");
            }

            if (!string.IsNullOrWhiteSpace(person.Gender))
            {
                QueryHelper.Where(ref sql, "[Person].[Gender] = @Gender");
            }

            if (person.Dob != null)
            {
                QueryHelper.Where(ref sql, "[Person].[Dob] = @Dob");
            }
        }

        public async Task<IEnumerable<Person>> GetAll(Person person)
        {
            var sql = SelectAllColumns();
            
            ApplySearch(ref sql, person);

            var param = new
                {FirstName = $"{QueryHelper.ParamStartsWith(person.FirstName)}", LastName = $"{QueryHelper.ParamStartsWith(person.LastName)}", person.Gender, person.Dob};

            return await ExecuteQuery(sql, param);
        }

        public async Task<int> GetNumberOfBirthdaysThisWeek(DateTime weekBeginning)
        {
            var sql = $"SELECT COUNT([Person].[Id]) FROM {TblName}";

            QueryHelper.Where(ref sql, "[Person].[Dob] >= @Monday");
            QueryHelper.Where(ref sql, "[Person].[Dob] <= @Sunday");

            return await ExecuteIntQuery(sql,
                new {Monday = weekBeginning, Sunday = weekBeginning.AddDays(6)});
        }

        protected override async Task<IEnumerable<Person>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Person, ApplicationUser, Person>(sql, (person, user) =>
            {
                person.User = user;

                return person;
            }, param);
        }
    }
}
