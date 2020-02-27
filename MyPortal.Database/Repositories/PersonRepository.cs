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
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class PersonRepository : BaseReadWriteRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
       RelatedColumns = $"{EntityHelper.GetUserColumns("User")}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[Person].[UserId]", "User")}";
        }

        public async Task Update(Person entity)
        {
            var personInDb = await Context.People.FindAsync(entity.Id);

            if (personInDb == null)
            {
                throw new Exception("Person not found.");
            }

            personInDb.Title = entity.Title;
            personInDb.FirstName = entity.FirstName;
            personInDb.MiddleName = entity.MiddleName;
            personInDb.PhotoId = entity.PhotoId;
            personInDb.NhsNumber = entity.NhsNumber;
            personInDb.LastName = entity.LastName;
            personInDb.Gender = entity.Gender;
            personInDb.Dob = entity.Dob;
            personInDb.Deceased = entity.Deceased;
            personInDb.UserId = entity.UserId;
            personInDb.Deleted = entity.Deleted;
        }

        public async Task<Person> GetByUserId(string userId)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[Person].[UserId] = @UserId");

            return (await ExecuteQuery(sql, new {UserId = userId})).Single();
        }

        private static void ApplySearch(ref string sql, Person person)
        {
            if (!string.IsNullOrWhiteSpace(person.FirstName))
            {
                SqlHelper.Where(ref sql, "[Person].[FirstName] LIKE @FirstName");
            }

            if (!string.IsNullOrWhiteSpace(person.LastName))
            {
                SqlHelper.Where(ref sql, "[Person].[LastName] LIKE @LastName");
            }

            if (!string.IsNullOrWhiteSpace(person.Gender))
            {
                SqlHelper.Where(ref sql, "[Person].[Gender] = @Gender");
            }

            if (person.Dob != null)
            {
                SqlHelper.Where(ref sql, "[Person].[Dob] = @Dob");
            }
        }

        public async Task<IEnumerable<Person>> Search(Person person)
        {
            var sql = SelectAllColumns();
            
            ApplySearch(ref sql, person);

            var param = new
                {FirstName = $"{SqlHelper.ParamStartsWith(person.FirstName)}", LastName = $"{SqlHelper.ParamStartsWith(person.LastName)}", person.Gender, person.Dob};

            return await ExecuteQuery(sql, param);
        }

        public async Task<int> GetNumberOfBirthdaysThisWeek(DateTime weekBeginning)
        {
            var sql = $"SELECT COUNT([Person].[Id]) FROM {TblName}";

            SqlHelper.Where(ref sql, "[Person].[Dob] >= @Monday");
            SqlHelper.Where(ref sql, "[Person].[Dob] <= @Sunday");

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
