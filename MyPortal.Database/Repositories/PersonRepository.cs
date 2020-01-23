using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        private const string TblName = "[dbo].[Person] AS [P]";

        public const string AllColumns =
            "[P].[Id],[P].[Title],[P].[FirstName],[P].[MiddleName],[P].[PhotoId],[P].[NhsNumber],[P].[LastName],[P].[Gender],[P].[Dob],[P].[Deceased],[P].[UserId],[P].[Deleted]";

        public PersonRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            return await Connection.QueryAsync<Person>(sql);
        }

        public async Task<Person> GetById(int id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [P].[Id] = @PersonId";

            return await Connection.QuerySingleOrDefaultAsync<Person>(sql, new {PersonId = id});
        }

        public void Create(Person entity)
        {
            Context.People.Add(entity);
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

        public async Task Delete(int id)
        {
            var personInDb = await Context.People.FindAsync(id);

            if (personInDb == null)
            {
                throw new Exception("Person not found.");
            }

            Context.People.Remove(personInDb);
        }

        public async Task<Person> GetByUserId(string userId)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [P].[UserId] = @UserId";

            return await Connection.QuerySingleOrDefaultAsync<Person>(sql, new {UserId = userId});
        }

        public async Task<IEnumerable<Person>> Search(Person person)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            var hasWhereClause = false;

            if (!string.IsNullOrWhiteSpace(person.FirstName))
            {
                sql = $"{sql} WHERE [P].[FirstName] LIKE @FirstName";

                hasWhereClause = true;
            }

            if (!string.IsNullOrWhiteSpace(person.LastName))
            {
                var appendCondition = hasWhereClause ? "AND" : "WHERE";

                sql = $"{sql} {appendCondition} [P].[LastName] Like @LastName";

                hasWhereClause = true;
            }

            if (!string.IsNullOrWhiteSpace(person.Gender))
            {
                var appendCondition = hasWhereClause ? "AND" : "WHERE";

                sql = $"{sql} {appendCondition} [P].[Gender] = @Gender";

                hasWhereClause = true;
            }

            if (person.Dob != null)
            {
                var appendCondition = hasWhereClause ? "AND" : "WHERE";

                sql = $"{sql} {appendCondition} WHERE [P].[Dob] = @Dob";
            }

            return await Connection.QueryAsync<Person>(sql,
                new
                {
                    FirstName = $"%{person.FirstName}%", LastName = $"%{person.LastName}%", Gender = person.Gender,
                    Dob = person.Dob
                });
        }

        public async Task<int> GetNumberOfBirthdaysThisWeek(DateTime weekBeginning)
        {
            var sql = $"SELECT COUNT([P].[Id]) FROM {TblName} WHERE [P].[Dob] >= @Monday AND [P].[Dob] <= @Sunday";

            return await Connection.QueryFirstOrDefaultAsync<int>(sql,
                new {Monday = weekBeginning, Sunday = weekBeginning.AddDays(6)});
        }
    }
}
