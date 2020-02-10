using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
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

        protected override Task<IEnumerable<Location>> ExecuteQuery(string sql, object param = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Location>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Location> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Location entity)
        {
            throw new NotImplementedException();
        }
    }
}
