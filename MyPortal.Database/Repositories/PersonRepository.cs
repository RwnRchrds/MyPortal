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
using MyPortal.Database.Search;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class PersonRepository : BaseReadWriteRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
     
        }

        public async Task<Person> GetByUserId(Guid userId)
        {
            var query = SelectAllColumns();

            query.Where("Person.UserId", userId);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(ApplicationUser), "User");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.AspNetUsers as User", "User.Id", "Person.UserId");
        }

        private static void ApplySearch(Query query, PersonSearch search)
        {
            if (!string.IsNullOrWhiteSpace(search.FirstName))
            {
                query.WhereStarts("Person.FirstName", search.FirstName);
            }

            if (!string.IsNullOrWhiteSpace(search.LastName))
            {
                query.WhereStarts("Person.LastName", search.LastName);
            }

            if (!string.IsNullOrWhiteSpace(search.Gender))
            {
                query.Where("Person.Gender", search.Gender);
            }

            if (search.Dob != null)
            {
                query.WhereDate("Person.Dob", search.Dob);
            }
        }

        public async Task<IEnumerable<Person>> GetAll(PersonSearch searchParams)
        {
            var query = SelectAllColumns();
            
            ApplySearch(query, searchParams);

            return await ExecuteQuery(query);
        }

        protected override async Task<IEnumerable<Person>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Person, ApplicationUser, Person>(sql.Sql, (person, user) =>
            {
                person.User = user;

                return person;
            }, sql.Bindings);
        }
    }
}
