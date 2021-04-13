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
    public class AddressPersonRepository : BaseReadWriteRepository<AddressPerson>, IAddressPersonRepository
    {
        public AddressPersonRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "Person");
            query.SelectAllColumns(typeof(Address), "Address");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("People as Person", "Person.Id", "AddressPerson.PersonId");
            query.LeftJoin("Addresses as Address", "Address.Id", "AddressPerson.AddressId");
        }

        protected override async Task<IEnumerable<AddressPerson>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<AddressPerson, Address, Person, AddressPerson>(sql.Sql,
                (ap, address, person) =>
                {
                    ap.Address = address;
                    ap.Person = person;

                    return ap;
                }, sql.NamedBindings, Transaction);
        }

        public async Task Update(AddressPerson entity)
        {
            var addressPerson = await Context.AddressPersons.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (addressPerson == null)
            {
                throw new EntityNotFoundException("Address person not found.");
            }

            addressPerson.AddressId = entity.AddressId;
            addressPerson.AddressTypeId = entity.AddressTypeId;
        }
    }
}