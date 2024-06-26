﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Person;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class PersonRepository : BaseReadWriteRepository<Person>, IPersonRepository
    {
        public PersonRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Ethnicities as E", "E.Id", $"{TblAlias}.EthnicityId");
            query.LeftJoin("Directories as D", "D.Id", $"{TblAlias}.DirectoryId");
            query.LeftJoin("Photos as PH", "PH.Id", $"{TblAlias}.PhotoId");

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

            var people = await DbUser.Transaction.Connection.QueryAsync<Person, Ethnicity, Directory, Photo, Person>(
                sql.Sql,
                (person, ethnicity, dir, photo) =>
                {
                    person.Ethnicity = ethnicity;
                    person.Directory = dir;
                    person.Photo = photo;

                    return person;
                }, sql.NamedBindings, DbUser.Transaction);

            return people;
        }

        protected virtual void IncludePersonTypes(Query query)
        {
            query.LeftJoin("Users as U", "U.PersonId", $"{TblAlias}.Id");
            query.LeftJoin("Students AS S", "S.PersonId", $"{TblAlias}.Id");
            query.LeftJoin("StaffMembers AS SM", "SM.PersonId", $"{TblAlias}.Id");
            query.LeftJoin("Contacts AS C", "C.PersonId", $"{TblAlias}.Id");

            query.SelectRaw("U.Id as UserId");
            query.SelectRaw("S.Id as StudentId");
            query.SelectRaw("SM.Id as StaffId");
            query.SelectRaw("C.Id as ContactId");
        }

        public async Task<Person> GetByUserId(Guid userId)
        {
            var query = GetDefaultQuery();

            query.LeftJoin("Users as U", "U.PersonId", $"{TblAlias}.Id");

            query.Where("U.Id", userId);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        public async Task<PersonSearchResult> GetPersonWithTypesById(Guid personId)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.Id", personId);

            return (await ExecuteQueryWithTypes(query)).FirstOrDefault();
        }

        public async Task<PersonSearchResult> GetPersonWithTypesByUserId(Guid userId)
        {
            var query = GetDefaultQuery();

            IncludePersonTypes(query);

            query.Where("U.Id", userId);

            return (await ExecuteQueryWithTypes(query)).FirstOrDefault();
        }

        public async Task<PersonSearchResult> GetPersonWithTypesByDirectoryId(Guid directoryId)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.DirectoryId", directoryId);

            return (await ExecuteQueryWithTypes(query)).FirstOrDefault();
        }

        public async Task<IEnumerable<Person>> GetAll(PersonSearchOptions searchParams)
        {
            var query = GetDefaultQuery();

            searchParams.ApplySearch(query, TblAlias);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<PersonSearchResult>> GetAllWithTypes(PersonSearchOptions searchParams)
        {
            var query = GetDefaultQuery();

            searchParams.ApplySearch(query, TblAlias);

            return await ExecuteQueryWithTypes(query);
        }

        protected async Task<IEnumerable<PersonSearchResult>> ExecuteQueryWithTypes(Query query)
        {
            IncludePersonTypes(query);

            var sql = Compiler.Compile(query);

            var people = await DbUser.Transaction.Connection
                .QueryAsync<Person, Ethnicity, Directory, Photo, PersonTypeIndicator, PersonSearchResult>(sql.Sql,
                    (person, ethnicity, dir, photo, types) =>
                    {
                        var result = new PersonSearchResult();
                        result.Person = person;
                        person.Ethnicity = ethnicity;
                        person.Directory = dir;
                        person.Photo = photo;
                        result.PersonTypes = types;

                        return result;
                    }, sql.NamedBindings, DbUser.Transaction, splitOn: "Id, Id, Id, IsUser");

            return people;
        }

        public async Task Update(Person entity)
        {
            var person = await DbUser.Context.People.FirstOrDefaultAsync(x => x.Id == entity.Id);

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
            person.Gender = entity.Gender;
            person.Dob = entity.Dob;
            person.Deceased = entity.Deceased;
            person.EthnicityId = entity.EthnicityId;
        }
    }
}