using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Query.Person;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class PersonRepository : BaseReadWriteRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
     
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Ethnicities", "E", "EthnicityId");
            JoinEntity(query, "Directory", "D", "DirectoryId");
            JoinEntity(query, "Photos", "PH", "PhotoId");

            return query;
        }
        
        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Ethnicity), "E");
            query.SelectAllColumns(typeof(Directory), "D");
            query.SelectAllColumns(typeof(Photo), "PH");

            return query;
        }

        protected override async Task<IEnumerable<Person>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var people = await Transaction.Connection.QueryAsync<Person, Ethnicity, Directory, Photo, Person>(sql.Sql,
                (person, ethnicity, dir, photo) =>
                {
                    person.Ethnicity = ethnicity;
                    person.Directory = dir;
                    person.Photo = photo;

                    return person;
                }, sql.NamedBindings, Transaction);

            return people;
        }

        public async Task<Person> GetByUserId(Guid userId)
        {
            var query = GenerateQuery();

            query.Where("Person.UserId", userId);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        private void ApplySearch(Query query, PersonSearchOptions search, bool includePersonTypes)
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
                query.LeftJoin("Users as U", "U.PersonId", $"{TblAlias}.Id");
                query.LeftJoin("Students AS S", "S.PersonId", $"{TblAlias}.Id");
                query.LeftJoin("StaffMembers AS ST", "ST.PersonId", $"{TblAlias}.Id");
                query.LeftJoin("Contacts AS C", "C.PersonId", $"{TblAlias}.Id");
                
                query.SelectRaw("CASE WHEN [User].[Id] IS NULL THEN 0 ELSE 1 END AS IsUser");
                query.SelectRaw("CASE WHEN [Student].[Id] IS NULL THEN 0 ELSE 1 END AS IsStudent");
                query.SelectRaw("CASE WHEN [StaffMember].[Id] IS NULL THEN 0 ELSE 1 END AS IsStaff");
                query.SelectRaw("CASE WHEN [Contact].[Id] IS NULL THEN 0 ELSE 1 END AS IsContact");
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

        protected async Task<IEnumerable<PersonSearchResult>> ExecuteQueryWithTypes(Query query)
        {
            var sql = Compiler.Compile(query);
            
            var people = await Transaction.Connection.QueryAsync<Person, Ethnicity, Directory, Photo, PersonTypeIndicator, PersonSearchResult>(sql.Sql,
                (person, ethnicity, dir, photo, types) =>
                {
                    var result = new PersonSearchResult();
                    result.Person = person;
                    person.Ethnicity = ethnicity;
                    person.Directory = dir;
                    person.Photo = photo;
                    result.PersonTypes = types;

                    return result;
                }, sql.NamedBindings, Transaction, splitOn:"Id, Id, Id, IsUser");

            return people;
        }

        public async Task Update(Person entity)
        {
            var person = await Context.People.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (person == null)
            {
                throw new EntityNotFoundException("Person not found.");
            }

            person.Title = entity.Title;
            person.FirstName = entity.FirstName;
            person.MiddleName = entity.MiddleName;
            person.LastName = entity.LastName;
            person.PhotoId = entity.PhotoId;
            person.NhsNumber = entity.NhsNumber;
            person.UpdatedDate = entity.UpdatedDate;
            person.Gender = entity.Gender;
            person.Dob = entity.Dob;
            person.Deceased = entity.Deceased;
            person.EthnicityId = entity.EthnicityId;
        }
    }
}
