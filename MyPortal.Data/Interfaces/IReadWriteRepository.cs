using System.Collections.Generic;

namespace MyPortal.Data.Interfaces
{
    public interface IReadWriteRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
