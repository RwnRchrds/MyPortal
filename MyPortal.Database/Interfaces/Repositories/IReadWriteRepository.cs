using System;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IReadWriteRepository<TEntity> : IReadRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetByIdWithTracking(Guid id);
        void Create(TEntity entity);
        Task Delete(Guid id);
        Task SaveChanges();
    }
}
