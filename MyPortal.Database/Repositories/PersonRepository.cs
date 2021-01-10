using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Query;
using MyPortal.Database.Models.Query.Person;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class PersonRepository : BaseReadWriteRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context) : base(context, "Person")
        {
     
        }

        public async Task<Person> GetByUserId(Guid userId)
        {
            var query = GenerateQuery();

            query.Where("Person.UserId", userId);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(User), "User");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Users as User", "User.PersonId", "Person.Id");
        }

        private static void ApplySearch(Query query, PersonSearchOptions search, bool includePersonTypes)
        {
            if (search != null)
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

            if (includePersonTypes)
            {
                query.SelectRaw("CASE WHEN [User].[Id] IS NULL THEN 0 ELSE 1 END AS IsUser");
                query.SelectRaw("CASE WHEN [Student].[Id] IS NULL THEN 0 ELSE 1 END AS IsStudent");
                query.SelectRaw("CASE WHEN [StaffMember].[Id] IS NULL THEN 0 ELSE 1 END AS IsStaff");
                query.SelectRaw("CASE WHEN [Contact].[Id] IS NULL THEN 0 ELSE 1 END AS IsContact");

                query.LeftJoin("dbo.Students AS Student", "Student.PersonId", "Person.Id");
                query.LeftJoin("dbo.StaffMembers AS StaffMember", "StaffMember.PersonId", "Person.Id");
                query.LeftJoin("dbo.Contacts AS Contact", "Contact.PersonId", "Person.Id");
            }
        }

        public async Task<PersonSearchResult> GetPersonWithTypesById(Guid personId)
        {
            var query = GenerateQuery();

            query.Where("Person.Id", personId);

            ApplySearch(query, null, true);

            return (await ExecuteQueryWithTypes(query)).FirstOrDefault();
        }

        public async Task<IEnumerable<Person>> GetAll(PersonSearchOptions searchParams)
        {
            var query = GenerateQuery();
            
            ApplySearch(query, searchParams, false);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<PersonSearchResult>> GetAllWithTypes(PersonSearchOptions searchParams)
        {
            var query = GenerateQuery();

            ApplySearch(query, searchParams, true);

            return await ExecuteQueryWithTypes(query);
        }

        protected override async Task<IEnumerable<Person>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Person, User, Person>(sql.Sql, (person, user) =>
            {
                person.User = user;

                return person;
            }, sql.NamedBindings);
        }

        protected async Task<IEnumerable<PersonSearchResult>> ExecuteQueryWithTypes(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Person, User, PersonTypeIndicator, PersonSearchResult>(sql.Sql,
                (person, user, types) =>
                {
                    var result = new PersonSearchResult();
                    person.User = user;
                    result.Person = person;
                    result.PersonTypes = types;

                    return result;
                }, sql.NamedBindings, splitOn:"Id, IsUser");
        }
    }
}
