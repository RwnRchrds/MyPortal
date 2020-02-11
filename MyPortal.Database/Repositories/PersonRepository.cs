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
        private readonly string RelatedColumns = $"{EntityHelper.GetUserColumns("User")}";

        private readonly string JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[Person].[UserId]", "User")}";

        public PersonRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";

            return await ExecuteQuery(sql);
        }

        public async Task<Person> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated} WHERE [Person].[Id] = @PersonId";

            return (await ExecuteQuery(sql, new {PersonId = id})).Single();
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
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [Person].[UserId] = @UserId";

            return (await ExecuteQuery(sql, new {UserId = userId})).Single();
        }

        public async Task<IEnumerable<Person>> Search(Person person)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";

            if (!string.IsNullOrWhiteSpace(person.FirstName))
            {
                sql = $"{SqlHelper.Where(WhereType.Like, sql, "[Person].[FirstName]", "@FirstName")}";
            }

            if (!string.IsNullOrWhiteSpace(person.LastName))
            {
                sql = $"{SqlHelper.Where(WhereType.Like, sql, "[Person].[LastName]", "@LastName")}";
            }

            if (!string.IsNullOrWhiteSpace(person.Gender))
            {
                sql = $"{SqlHelper.Where(WhereType.Equals, sql, "[Person].[Gender]", "@Gender")}";
            }

            if (person.Dob != null)
            {
                sql = $"{SqlHelper.Where(WhereType.Equals, sql, "[Person].[Dob]", "@Dob")}";
            }

            var param = new
                {FirstName = $"{person.FirstName}%", LastName = $"{person.LastName}%", person.Gender, person.Dob};

            return await ExecuteQuery(sql, param);
        }

        public async Task<int> GetNumberOfBirthdaysThisWeek(DateTime weekBeginning)
        {
            var sql = $"SELECT COUNT([Person].[Id]) FROM {TblName} WHERE [Person].[Dob] >= @Monday AND [Person].[Dob] <= @Sunday";

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
