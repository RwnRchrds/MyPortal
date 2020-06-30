using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IReadRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
    }
}
