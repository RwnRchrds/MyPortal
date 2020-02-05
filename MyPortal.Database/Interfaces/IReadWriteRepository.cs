using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces
{
    public interface IReadWriteRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdWithTracking(int id);
        void Create(TEntity entity);
        Task Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveChanges();
    }
}
