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

        public async Task<IEnumerable<Address>> GetByPerson(int personId)
        {
            var sql =
                $"SELECT {AllColumns} FROM {TblName} {SqlHelper.Join(JoinType.InnerJoin, "[dbo].[AddressPerson]", "[AddressPerson].[PersonId]", "@PersonId")}";

            return await ExecuteQuery(sql, new {PersonId = personId});
        }
    }
}
