using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces
{
    public interface IReadWriteRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdWithTracking(Guid id);
        void Create(TEntity entity);
        Task Delete(Guid id);
        Task SaveChanges();
    }
}
