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

        public Task Update(Address entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Address>> GetAddressesByPerson(int personId)
        {
            throw new NotImplementedException();
        }
    }
}
