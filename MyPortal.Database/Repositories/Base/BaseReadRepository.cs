using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore.Storage;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using SqlKata;
using SqlKata.Compilers;

namespace MyPortal.Database.Repositories.Base
{
    public abstract class BaseReadRepository<TEntity> : BaseRepository, IReadRepository<TEntity> where TEntity : class, IEntity
    {
        public BaseReadRepository(DbTransaction transaction, string tblAlias = null) : base(transaction)
        {
            TblName = EntityHelper.GetTableName(typeof(TEntity), out TblAlias, tblAlias);
        }

        protected string TblName;

        protected string TblAlias;

        protected Query JoinEntity(Query query, string tblName, string alias, string foreignKey, string op = "=", string joinType = "left join")
        {
            query.Join($"{tblName} as {alias}", $"{alias}.Id", $"{TblAlias}.{foreignKey}", op, joinType);

            return query;
        }

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

            return await Transaction.Connection.QueryAsync<T>(sql.Sql, sql.NamedBindings, Transaction);
        }

        protected async Task<TEntity> ExecuteQueryFirstOrDefault(Query query)
        {
            var result = await ExecuteQuery(query);

            return result.FirstOrDefault();
        }

        protected Query GenerateQuery(bool includeSoftDeleted = false)
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

        protected Query GenerateQuery(Type t, bool includeSoftDeleted = false)
        {
            var tblName = EntityHelper.GetTableName(t, out string tblAlias);
            
            var query = new Query($"{tblName} as {tblAlias}").SelectAllColumns(t,tblAlias);

            if (t.GetInterfaces().Contains(typeof(ISoftDeleteEntity)) && !includeSoftDeleted)
            {
                query.Where($"{tblAlias}.Deleted", false);
            }

            return query;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var sql = GenerateQuery();

            return await ExecuteQuery(sql);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.Id", "=", id);

            return (await ExecuteQuery(query)).SingleOrDefault();
        }

        protected async Task<T> ExecuteQueryFirstOrDefault<T>(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryFirstOrDefaultAsync<T>(sql.Sql, sql.NamedBindings, Transaction);
        }

        protected Query GenerateEmptyQuery()
        {
            return GenerateEmptyQuery(typeof(TEntity), TblAlias);
        }

        protected Query GenerateEmptyQuery(Type t, string alias = null)
        {
            var tableName = EntityHelper.GetTableName(t);
            var table = string.IsNullOrWhiteSpace(alias) ? tableName : $"{tableName} as {alias}";
            return new Query(table);
        }

        protected async Task<int?> ExecuteQueryIntResult(Query query)
        {
            var sql = Compiler.Compile(query);

            var result = await Transaction.Connection.QueryFirstOrDefaultAsync<int?>(sql.Sql, sql.NamedBindings, Transaction);
            return result;
        }

        protected async Task<string> ExecuteQueryStringResult(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QuerySingleOrDefaultAsync<string>(sql.Sql, sql.NamedBindings, Transaction);
        }
    }
}
