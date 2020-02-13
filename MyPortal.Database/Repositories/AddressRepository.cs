using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AddressRepository : BaseReadWriteRepository<Address>, IAddressRepository
    {
        public AddressRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override async Task<IEnumerable<Address>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Address>(sql, param);
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            return await ExecuteQuery(sql);
        }

        public async Task<Address> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            SqlHelper.Where(ref sql, "[Address].[Id] = @AddressId");

            return (await ExecuteQuery(sql, new {AddressId = id})).Single();
        }

        public async Task Update(Address entity)
        {
            var addressInDb = await Context.Addresses.FindAsync(entity.Id);

            addressInDb.HouseName = entity.HouseNumber;
            addressInDb.HouseName = entity.HouseName;
            addressInDb.Apartment = entity.Apartment;
            addressInDb.Street = entity.Street;
            addressInDb.District = entity.District;
            addressInDb.Town = entity.Town;
            addressInDb.County = entity.County;
            addressInDb.Postcode = entity.Postcode;
            addressInDb.Country = entity.Country;
            addressInDb.Validated = entity.Validated;
        }

        public async Task<IEnumerable<Address>> GetAddressesByPerson(int personId)
        {
            var sql =
                $"SELECT {AllColumns} FROM {TblName} {SqlHelper.Join(JoinType.InnerJoin, "[dbo].[AddressPerson]", "[AddressPerson].[PersonId]", "@PersonId")}";

            return await ExecuteQuery(sql, new {PersonId = personId});
        }
    }
}
