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
        Task<TEntity> GetById(int id);

        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAll<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy);

        Task<IEnumerable<TEntity>> GetAll<TOrderBy, TThenBy>(Expression<Func<TEntity, TOrderBy>> orderBy, Expression<Func<TEntity, TThenBy>> thenBy);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
    }
}
