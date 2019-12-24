﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyPortal.Data.Interfaces
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);

        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAll<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy);

        Task<IEnumerable<TEntity>> GetAll<TOrderBy, TThenBy>(Expression<Func<TEntity, TOrderBy>> orderBy, Expression<Func<TEntity, TThenBy>> thenBy);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
    }
}