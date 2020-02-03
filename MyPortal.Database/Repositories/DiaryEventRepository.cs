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
    public class DiaryEventRepository : BaseRepository, IDiaryEventRepository
    {
        public DiaryEventRepository(IDbConnection connection) : base(connection)
        {
        }

        public Task<IEnumerable<DiaryEvent>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<DiaryEvent> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DiaryEvent> GetByIdWithTracking(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(DiaryEvent entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(DiaryEvent entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DiaryEvent entity)
        {
            throw new NotImplementedException();
        }
    }
}
