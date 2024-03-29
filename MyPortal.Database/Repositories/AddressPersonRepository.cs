﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AddressPersonRepository : BaseReadWriteRepository<AddressPerson>, IAddressPersonRepository
    {
        public AddressPersonRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Addresses as A", "A.Id", $"{TblAlias}.AddressId");
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Address), "A");
            query.SelectAllColumns(typeof(Person), "P");

            return query;
        }

        protected override async Task<IEnumerable<AddressPerson>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var addressPeople =
                await DbUser.Transaction.Connection.QueryAsync<AddressPerson, Address, Person, AddressPerson>(
                    sql.Sql,
                    (addressPerson, address, person) =>
                    {
                        addressPerson.Address = address;
                        addressPerson.Person = person;

                        return addressPerson;
                    }, sql.NamedBindings, DbUser.Transaction);

            return addressPeople;
        }

        public async Task Update(AddressPerson entity)
        {
            var addressPerson = await DbUser.Context.AddressPeople.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (addressPerson == null)
            {
                throw new EntityNotFoundException("Address person not found.");
            }

            addressPerson.AddressId = entity.AddressId;
            addressPerson.PersonId = entity.PersonId;
            addressPerson.AddressTypeId = entity.AddressTypeId;
            addressPerson.Main = entity.Main;
        }

        public async Task<IEnumerable<AddressPerson>> GetByPerson(Guid personId)
        {
            var query = GetDefaultQuery();

            query.Where("P.Id", personId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<AddressPerson>> GetByAddress(Guid addressId)
        {
            var query = GetDefaultQuery();

            query.Where("A.Id", addressId);

            return await ExecuteQuery(query);
        }
    }
}