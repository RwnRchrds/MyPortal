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
        public AddressRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<Address>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Address>(sql, param);
        }

        public async Task Update(Address entity)
        {
            var addressInDb = await GetByIdWithTracking(entity.Id);

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
