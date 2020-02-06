using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class PersonRepository : BaseReadWriteRepository<Person>, IPersonRepository
    {
        private readonly string TblName = @"[dbo].[Person] AS [Person]";

        internal static readonly string AllColumns = EntityHelper.GetAllColumns(typeof(Person), "Person");

        public PersonRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            return await Connection.QueryAsync<Person>(sql);
        }

        public async Task<Person> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [Person].[Id] = @PersonId";

            return await Connection.QuerySingleOrDefaultAsync<Person>(sql, new {PersonId = id});
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

            return await Connection.QuerySingleOrDefaultAsync<Person>(sql, new {UserId = userId});
        }

        public async Task<IEnumerable<Person>> Search(Person person)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            var hasWhereClause = false;

            if (!string.IsNullOrWhiteSpace(person.FirstName))
            {
                sql = $"{sql} WHERE [Person].[FirstName] LIKE @FirstName";

                hasWhereClause = true;
            }

            if (!string.IsNullOrWhiteSpace(person.LastName))
            {
                var appendCondition = hasWhereClause ? "AND" : "WHERE";

                sql = $"{sql} {appendCondition} [Person].[LastName] Like @LastName";

                hasWhereClause = true;
            }

            if (!string.IsNullOrWhiteSpace(person.Gender))
            {
                var appendCondition = hasWhereClause ? "AND" : "WHERE";

                sql = $"{sql} {appendCondition} [Person].[Gender] = @Gender";

                hasWhereClause = true;
            }

            if (person.Dob != null)
            {
                var appendCondition = hasWhereClause ? "AND" : "WHERE";

                sql = $"{sql} {appendCondition} WHERE [Person].[Dob] = @Dob";
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
            var sql = $"SELECT COUNT([Person].[Id]) FROM {TblName} WHERE [Person].[Dob] >= @Monday AND [Person].[Dob] <= @Sunday";

            return await Connection.QueryFirstOrDefaultAsync<int>(sql,
                new {Monday = weekBeginning, Sunday = weekBeginning.AddDays(6)});
        }

        protected override async Task<IEnumerable<Person>> ExecuteQuery(string sql, object param = null)
        {
            throw new NotImplementedException();
        }
    }
}
