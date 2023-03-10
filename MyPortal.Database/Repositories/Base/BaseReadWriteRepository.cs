using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

            return await Transaction.Connection.ExecuteAsync(compiled.Sql, compiled.NamedBindings, Transaction);
        }

        public void Create(TEntity entity)
        {
            var result = Context.Set<TEntity>().Add(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);

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
