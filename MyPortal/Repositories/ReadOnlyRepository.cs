using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        protected readonly MyPortalDbContext Context;

        public ReadOnlyRepository(MyPortalDbContext context)
        {
            Context = context;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> Get<TOrderBy>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TOrderBy>> orderBy, params string[] includes)
        {
            var query = Context.Set<TEntity>().Where(predicate).OrderBy(orderBy).AsQueryable();

            foreach (var property in includes)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable();

            foreach (var property in includes)
            {
                query = query.Include(property);
            }

            return await query.SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy)
        {
            return await Context.Set<TEntity>().OrderBy(orderBy).ToListAsync();
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }
    }
}