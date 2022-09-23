using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AddressLinkRepository : BaseReadWriteRepository<AddressLink>, IAddressLinkRepository
    {
        public AddressLinkRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Addresses as A", "A.Id", $"{TblAlias}.AddressId");
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");
            query.LeftJoin("Agencies as AG", "AG.Id", $"{TblAlias}.AgencyId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Address), "A");
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(Agency), "AG");

            return query;
        }

        protected override async Task<IEnumerable<AddressLink>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var addressPeople = await Transaction.Connection.QueryAsync<AddressLink, Address, Person, AddressLink>(
                sql.Sql,
                (addressPerson, address, person) =>
                {
                    addressPerson.Address = address;
                    addressPerson.Person = person;

                    return addressPerson;
                }, sql.NamedBindings, Transaction);

            return addressPeople;
        }

        public async Task Update(AddressLink entity)
        {
            var addressPerson = await Context.AddressLinks.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (addressPerson == null)
            {
                throw new EntityNotFoundException("Address person not found.");
            }

            addressPerson.AddressId = entity.AddressId;
            addressPerson.PersonId = entity.PersonId;
            addressPerson.AgencyId = entity.AgencyId;
            addressPerson.AddressTypeId = entity.AddressTypeId;
            addressPerson.Main = entity.Main;
        }
    }
}