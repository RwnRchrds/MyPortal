using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces
{
    public interface IReadWriteRepository<TEntity, TKey> : IReadRepository<TEntity, TKey> where TEntity : class
    {
        void Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TKey id);
    }
}
