﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Extensions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        protected readonly MyPortalDbContext Context;

        protected ReadOnlyRepository(MyPortalDbContext context)
        {
            Context = context;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy)
        {
            return await Context.Set<TEntity>().OrderBy(orderBy).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll<TOrderBy, TThenBy>(Expression<Func<TEntity, TOrderBy>> orderBy, Expression<Func<TEntity, TThenBy>> thenBy)
        {
            return await Context.Set<TEntity>().OrderBy(orderBy).ThenBy(thenBy).ToListAsync();
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }
    }
}