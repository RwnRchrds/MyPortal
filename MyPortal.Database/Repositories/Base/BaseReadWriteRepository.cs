using System;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Constants;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using Newtonsoft.Json;
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

            WriteAudit(entity.Id, AuditActions.Create, null);
        }

        protected void WriteAudit(Guid entityId, Guid action, TEntity oldValue)
        {
            WriteAuditRaw(entityId, action, oldValue != null ? JsonConvert.SerializeObject(oldValue) : null);
        }

        protected void WriteAuditRaw(Guid entityId, Guid action, string oldValue)
        {
            if (DbUser.AuditEnabled)
            {
                DbUser.Context.AuditLogs.Add(new AuditLog
                {
                    TableName = TblName,
                    EntityId = entityId,
                    AuditActionId = action,
                    CreatedDate = DateTime.Now,
                    UserId = DbUser.UserId,
                    OldValue = oldValue
                });
            }
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
            
            WriteAudit(id, AuditActions.Delete, entity);
        }
    }
}
