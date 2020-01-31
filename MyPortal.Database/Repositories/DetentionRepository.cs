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
    public class DetentionRepository : BaseRepository, IDetentionRepository
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

        public void Create(Detention entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Detention entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Detention entity)
        {
            throw new NotImplementedException();
        }
    }
}
