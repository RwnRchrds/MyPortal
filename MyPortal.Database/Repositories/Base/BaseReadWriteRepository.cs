using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories.Base
{
    public abstract class BaseReadWriteRepository<TEntity> : BaseReadRepository<TEntity>, IReadWriteRepository<TEntity>, IWriteRepository where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext Context;
        protected readonly List<Query> PendingQueries;

        protected BaseReadWriteRepository(ApplicationDbContext context, string tblAlias = null) : base(context.Database.GetDbConnection(), tblAlias)
        {
            Context = context;
            PendingQueries = new List<Query>();
        }

        protected async Task<int> ExecuteNonQuery(Query query)
        {
            var compiled = Compiler.Compile(query);

            return await Connection.ExecuteAsync(compiled.Sql, compiled.NamedBindings);
        }

        public async Task SaveChanges()
        {
            foreach (var query in PendingQueries)
            {
                await ExecuteNonQuery(query);
            }

            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdForEditing(Guid id)
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
            var entity = await GetByIdForEditing(id);

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
    }
}
