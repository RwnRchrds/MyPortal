using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Interfaces
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
