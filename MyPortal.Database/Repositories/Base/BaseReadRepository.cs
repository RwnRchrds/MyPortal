using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using SqlKata;
using Task = MyPortal.Database.Models.Entity.Task;

namespace MyPortal.Database.Repositories.Base
{
    public abstract class BaseReadRepository<TEntity> : BaseRepository, IReadRepository<TEntity> where TEntity : class, IEntity
    {
        public BaseReadRepository(DbUser dbUser, string tblAlias = null) : base(dbUser)
        {
            TblName = EntityHelper.GetTableName(typeof(TEntity), out TblAlias, tblAlias);
        }

        protected string TblName;

        protected string TblAlias;

        protected virtual Query JoinRelated(Query query)
        {
            return query;
        }

        protected virtual Query SelectAllRelated(Query query)
        {
            return query;
        }

        protected virtual async Task<IEnumerable<TEntity>> ExecuteQuery(Query query)
        {
            return await ExecuteQuery<TEntity>(query);
        }

        protected async Task<IEnumerable<T>> ExecuteQuery<T>(Query query)
        {
            var sql = Compiler.Compile(query);

            return await DbUser.Transaction.Connection.QueryAsync<T>(sql.Sql, sql.NamedBindings, DbUser.Transaction);
        }

        protected async Task<TEntity> ExecuteQueryFirstOrDefault(Query query)
        {
            var result = await ExecuteQuery(query);

            return result.FirstOrDefault();
        }

        protected virtual Query GetDefaultQuery(bool includeSoftDeleted = false)
        {
            var query = new Query($"{TblName} as {TblAlias}").SelectAllColumns(typeof(TEntity), TblAlias);
            
            JoinRelated(query);
            SelectAllRelated(query);

            if (typeof(TEntity).GetInterfaces().Contains(typeof(ISoftDeleteEntity)) && !includeSoftDeleted)
            {
                query.Where($"{TblAlias}.Deleted", false);
            }

            return query;
        }

        protected Query GetDefaultQuery(Type t, bool includeSoftDeleted = false)
        {
            var tblName = EntityHelper.GetTableName(t, out string tblAlias);
            
            var query = new Query($"{tblName} as {tblAlias}").SelectAllColumns(t,tblAlias);

            if (t.GetInterfaces().Contains(typeof(ISoftDeleteEntity)) && !includeSoftDeleted)
            {
                query.Where($"{tblAlias}.Deleted", false);
            }

            return query;
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsById(Guid id)
        {
            var query = GetDefaultQuery(typeof(AuditLog));

            query.LeftJoin("Users as U", "U.Id", "AL.UserId")
                .LeftJoin("AuditActions as AA", "AA.Id", "AL.AuditActionId");

            query.SelectAllColumns(typeof(User), "U");
            query.SelectAllColumns(typeof(AuditAction), "AA");

            query.Where("AL.TableName", TblName);
            query.Where("AL.EntityId", id);

            var sql = Compiler.Compile(query);
            
            return await DbUser.Transaction.Connection.QueryAsync<AuditLog, User, AuditAction, AuditLog>(sql.Sql,
                (auditLog, user, auditAction) =>
                {
                    auditLog.User = user;
                    auditLog.Action = auditAction;
                    return auditLog;
                }, sql.NamedBindings, DbUser.Transaction);
        }

        public async Task<IEnumerable<AuditLog>> GetAllAuditLogs()
        {
            var query = GetDefaultQuery(typeof(AuditLog));
            
            query.LeftJoin("Users as U", "U.Id", "AL.UserId")
                .LeftJoin("AuditActions as AA", "AA.Id", "AL.AuditActionId");

            query.SelectAllColumns(typeof(User), "U");
            query.SelectAllColumns(typeof(AuditAction), "AA");

            var sql = Compiler.Compile(query);
            
            return await DbUser.Transaction.Connection.QueryAsync<AuditLog, User, AuditAction, AuditLog>(sql.Sql,
                (auditLog, user, auditAction) =>
                {
                    auditLog.User = user;
                    auditLog.Action = auditAction;
                    return auditLog;
                }, sql.NamedBindings, DbUser.Transaction);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var sql = GetDefaultQuery();

            return await ExecuteQuery(sql);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.Id", id);

            return (await ExecuteQuery(query)).SingleOrDefault();
        }

        protected async Task<T> ExecuteQueryFirstOrDefault<T>(Query query)
        {
            var sql = Compiler.Compile(query);

            return await DbUser.Transaction.Connection.QueryFirstOrDefaultAsync<T>(sql.Sql, sql.NamedBindings,
                DbUser.Transaction);
        }

        protected Query GetEmptyQuery()
        {
            return GetEmptyQuery(typeof(TEntity), TblAlias);
        }

        protected Query GetEmptyQuery(Type t, string alias = null)
        {
            var tableName = EntityHelper.GetTableName(t);
            var table = string.IsNullOrWhiteSpace(alias) ? tableName : $"{tableName} as {alias}";
            return new Query(table);
        }

        protected async Task<int?> ExecuteQueryIntResult(Query query)
        {
            var sql = Compiler.Compile(query);

            var result =
                await DbUser.Transaction.Connection.QueryFirstOrDefaultAsync<int?>(sql.Sql, sql.NamedBindings,
                    DbUser.Transaction);
            return result;
        }

        protected async Task<string> ExecuteQueryStringResult(Query query)
        {
            var sql = Compiler.Compile(query);

            return await DbUser.Transaction.Connection.QuerySingleOrDefaultAsync<string>(sql.Sql, sql.NamedBindings,
                DbUser.Transaction);
        }
    }
}
