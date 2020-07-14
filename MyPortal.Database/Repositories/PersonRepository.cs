using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Dapper;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
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
            query.LeftJoin("AspNetUsers as User", "User.Id", "Person.UserId");
        }

        private static void ApplySearch(Query query, PersonSearchOptions search)
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

        public async Task<PersonTypeIndicator> GetPersonTypeIndicatorById(Guid personId)
        {
            var indicator = new PersonTypeIndicator();

            var userQuery = new Query("Person").Where(q => q.WhereNotNull("Person.UserId").Where("Person.Id", personId)).AsCount();
            var studentQuery = new Query("Student").Where("Student.PersonId", personId).AsCount();
            var employeeQuery = new Query("StaffMember").Where("StaffMember.PersonId", personId).AsCount();
            var contactQuery = new Query("Contact").Where("Contact.PersonId", personId).AsCount();

            // TODO: Agent and applicant queries when available

            var userSql = Compiler.Compile(userQuery);
            var studentSql = Compiler.Compile(studentQuery);
            var employeeSql = Compiler.Compile(employeeQuery);
            var contactSql = Compiler.Compile(contactQuery);

            using (var multi =
                await Connection.QueryMultipleAsync($"{userSql.Sql};{studentSql.Sql};{employeeSql.Sql};{contactSql}",
                    userSql.NamedBindings))
            {
                indicator.User = await multi.ReadFirstAsync<int>() > 0;
                indicator.Student = await multi.ReadFirstAsync<int>() > 0;
                indicator.Employee = await multi.ReadFirstAsync<int>() > 0;
                indicator.Contact = await multi.ReadFirstAsync<int>() > 0;
            }

            return indicator;
        }

        public async Task<IEnumerable<Person>> GetAll(PersonSearchOptions searchParams)
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
            }, sql.NamedBindings);
        }
    }
}
