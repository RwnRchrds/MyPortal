using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Constants;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces;
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
            var entity = await Context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                throw new Exception($"{typeof(TEntity).Name} with ID {id} not found.");
            }

            return entity;
        }

        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetByIdWithTracking(id);

            switch (entity)
            {
                case ISystemEntity systemObject when systemObject.System:
                    throw new SystemEntityException("You cannot delete a system entity.");
                case ISoftDelete softDeleteObject:
                    softDeleteObject.Deleted = true;
                    break;
                default:
                    Context.Set<TEntity>().Remove(entity);
                    break;
            }
        }

        public new void Dispose()
        {
            Context.Dispose();
            Connection.Dispose();
        }
    }
}
