using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
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

        protected BaseReadWriteRepository(ApplicationDbContext context, DbTransaction transaction, string tblAlias = null) : base(transaction, tblAlias)
        {
            Context = context;
        }

        protected async Task<int> ExecuteNonQuery(Query query)
        {
            var compiled = Compiler.Compile(query);

            var result = await Transaction.Connection.ExecuteAsync(compiled.Sql, compiled.NamedBindings, Transaction);

            return result;
        }

        public void Create(TEntity entity)
        {
            if (entity is IReadOnlyEntity)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }

            var result = Context.Set<TEntity>().Add(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);

            switch (entity)
            {
                case IReadOnlyEntity:
                case ISystemEntity {System: true}:
                    throw ExceptionHelper.DeleteSystemEntityException;
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
