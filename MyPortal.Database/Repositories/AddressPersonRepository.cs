using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AddressPersonRepository : BaseReadWriteRepository<AddressPerson>, IAddressPersonRepository
    {
        private readonly string RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Person))},
{EntityHelper.GetAllColumns(typeof(Address))}";

        private readonly string JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[AddressPerson].[PersonId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Address]", "[Address].[Id]", "[AddressPerson].[AddressId]")}";
        
        public AddressPersonRepository(IDbConnection connection) : base(connection)
        {
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

        public async Task<IEnumerable<AddressPerson>> GetAll()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";

            return await ExecuteQuery(sql);
        }

        public async Task<AddressPerson> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";
            
            SqlHelper.Where(ref sql, "[AddressPerson].[Id] = @AddressPersonId");

            return (await ExecuteQuery(sql, new {AddressPersonId = id})).Single();
        }

        public async Task Update(AddressPerson entity)
        {
            var apInDb = await Context.AddressPersons.FindAsync(entity.Id);

            apInDb.AddressId = entity.AddressId;
            apInDb.PersonId = entity.PersonId;
        }
    }
}