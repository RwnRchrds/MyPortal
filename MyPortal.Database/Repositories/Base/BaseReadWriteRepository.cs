using System;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories.Base
{
    public abstract class BaseReadWriteRepository<TEntity> : BaseReadRepository<TEntity>, IReadWriteRepository<TEntity> where TEntity : class, IEntity
    {
        protected BaseReadWriteRepository(DbUserWithContext dbUserWithContext, string tblAlias = null) : base(dbUserWithContext, tblAlias)
        {
            
        }

        protected override DbUserWithContext DbUser => base.DbUser as DbUserWithContext;

        protected async Task<int> ExecuteNonQuery(Query query)
        {
            var compiled = Compiler.Compile(query);

            var result = await DbUser.Transaction.Connection.ExecuteAsync(compiled.Sql, compiled.NamedBindings, DbUser.Transaction);

            return result;
        }

        public void Create(TEntity entity)
        {
            if (entity is IReadOnlyEntity)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }
            
            var result = DbUser.Context.Set<TEntity>().Add(entity);
        }

        public async Task Delete(Guid id)
        {
            var entity = await DbUser.Context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                throw new EntityNotFoundException($"Entity with ID {id} was not found.");
            }
            
            switch (entity)
            {
                case IReadOnlyEntity:
                case ISystemEntity {System: true}:
                    throw ExceptionHelper.DeleteSystemEntityException;
                case ISoftDeleteEntity softDeleteObject:
                    softDeleteObject.Deleted = true;
                    break;
                default:
                    DbUser.Context.Set<TEntity>().Remove(entity);
                    break;
            }
        }
    }
}
