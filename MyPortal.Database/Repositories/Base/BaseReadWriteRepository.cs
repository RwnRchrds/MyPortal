﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories.Base
{
    public abstract class BaseReadWriteRepository<TEntity> : BaseReadRepository<TEntity>, IReadWriteRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext Context;
        protected readonly List<Query> PendingQueries;

        protected BaseReadWriteRepository(ApplicationDbContext context, IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            Context = context;
            PendingQueries = new List<Query>();
        }

        public async Task SaveChanges()
        {
            foreach (var query in PendingQueries)
            {
                await ExecuteNonQuery(query);
            }

            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdWithTracking(Guid id)
        {
            var entity = await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception($"{typeof(TEntity).Name} with ID {id} not found.");
            }

            return entity;
        }

        public void Create(TEntity entity)
        {
            var result = Context.Set<TEntity>().Add(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetByIdWithTracking(id);

            switch (entity)
            {
                case ISystemEntity systemObject when systemObject.System:
                    throw new SystemEntityException("System entities cannot be deleted.");
                case ISoftDeleteEntity softDeleteObject:
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
