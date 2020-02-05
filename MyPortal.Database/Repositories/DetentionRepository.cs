using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class DetentionRepository : BaseReadWriteRepository<Detention>, IDetentionRepository
    {
        public DetentionRepository(IDbConnection connection) : base(connection)
        {
        }

        public Task<IEnumerable<Detention>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Detention> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Detention entity)
        {
            throw new NotImplementedException();
        }

        protected override async Task<IEnumerable<Detention>> ExecuteQuery(string sql, object param = null)
        {
            throw new NotImplementedException();
        }
    }
}
