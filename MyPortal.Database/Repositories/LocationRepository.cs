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
    public class LocationRepository : BaseReadWriteRepository<Location>, ILocationRepository
    {
        public LocationRepository(IDbConnection connection) : base(connection)
        {
        }

        protected override async Task<IEnumerable<Location>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Location>(sql, param);
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            return await ExecuteQuery(sql);
        }

        public async Task<Location> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [Location].[Id] = @LocationId";

            return (await ExecuteQuery(sql, new {LocationId = id})).Single();
        }

        public async Task Update(Location entity)
        {
            var locationInDb = await Context.Locations.FindAsync(entity.Id);

            locationInDb.Description = entity.Description;
        }
    }
}
