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
    public class LocationRepository : BaseReadWriteRepository<Location>, ILocationRepository
    {
        public LocationRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<Location>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Location>(sql, param);
        }
    }
}
