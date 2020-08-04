using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AddressPersonRepository : BaseReadWriteRepository<AddressPerson>, IAddressPersonRepository
    {
        public AddressPersonRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Person), "Person");
            query.SelectAll(typeof(Address), "Address");

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

            return await Connection.QueryAsync<AddressPerson, Address, Person, AddressPerson>(sql.Sql,
                (ap, address, person) =>
                {
                    ap.Address = address;
                    ap.Person = person;

                    return ap;
                }, sql.NamedBindings);
        }
    }
}