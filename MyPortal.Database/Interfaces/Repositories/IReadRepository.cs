using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IReadRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
    }
}
