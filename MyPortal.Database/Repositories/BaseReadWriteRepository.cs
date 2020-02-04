using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Database.Repositories
{
    public abstract class BaseReadWriteRepository<TEntity> : BaseReadRepository<TEntity> where TEntity : class
    {
        protected BaseReadWriteRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdWithTracking(params object[] keyValues)
        {
            return await Context.Set<TEntity>().FindAsync(keyValues);
        }

        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
