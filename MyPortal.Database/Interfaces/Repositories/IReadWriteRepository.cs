using System;
using System.Threading.Tasks;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IReadWriteRepository<TEntity> : IReadRepository<TEntity> where TEntity : class, IEntity
    {
        void Create(TEntity entity);
        Task Delete(Guid id);
    }
}
