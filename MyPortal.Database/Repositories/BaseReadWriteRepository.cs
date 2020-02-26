using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public abstract class BaseReadWriteRepository<TEntity> : BaseReadRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext Context;

        protected BaseReadWriteRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, tblAlias)
        {
            Context = context;
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdWithTracking(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public new void Dispose()
        {
            Context.Dispose();
            Connection.Dispose();
        }
    }
}
