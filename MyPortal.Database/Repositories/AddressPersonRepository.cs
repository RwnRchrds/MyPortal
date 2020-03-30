using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class AddressPersonRepository : BaseReadWriteRepository<AddressPerson>, IAddressPersonRepository
    {
        public AddressPersonRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Person))},
{EntityHelper.GetAllColumns(typeof(Address))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[AddressPerson].[PersonId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Address]", "[Address].[Id]", "[AddressPerson].[AddressId]")}";
        }

        protected override async Task<IEnumerable<AddressPerson>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<AddressPerson, Address, Person, AddressPerson>(sql,
                (ap, address, person) =>
                {
                    ap.Address = address;
                    ap.Person = person;

                    return ap;
                }, param);
        }
    }
}