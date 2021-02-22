﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using SqlKata;
using SqlKata.Compilers;

namespace MyPortal.Database.Repositories.Base
{
    public abstract class BaseReadRepository<TEntity> : BaseRepository, IReadRepository<TEntity> where TEntity : class, IEntity
    {
        public BaseReadRepository(IDbConnection connection, string tblAlias = null) : base(connection)
        {
            TblName = EntityHelper.GetTableName(typeof(TEntity), out TblAlias, tblAlias);
        }

        protected string TblName;

        protected string TblAlias;

        protected virtual async Task<IEnumerable<TEntity>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<TEntity>(sql.Sql, sql.NamedBindings);
        }

        protected async Task<TEntity> ExecuteQueryFirstOrDefault(Query query)
        {
            var result = await ExecuteQuery(query);

            return result.FirstOrDefault();
        }

        protected Query GenerateQuery(bool includeSoftDeleted = false, bool getRelated = true)
        {
            var query = new Query(TblName).SelectAllColumns(typeof(TEntity), TblAlias);

            if (typeof(TEntity).GetInterfaces().Contains(typeof(ISoftDeleteEntity)) && !includeSoftDeleted)
            {
                query.Where($"{TblAlias}.Deleted", false);
            }

            if (getRelated)
            {
                SelectAllRelated(query);
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

            return await Connection.QueryFirstOrDefaultAsync<T>(sql.Sql, sql.NamedBindings);
        }

        protected virtual void JoinRelated(Query query)
        {

        }

        protected virtual void SelectAllRelated(Query query)
        {
            JoinRelated(query);
        }

        protected Query GenerateEmptyQuery(Type t, string alias = null)
        {
            return new Query(EntityHelper.GetTableName(t, alias));
        }

        protected async Task<int?> ExecuteQueryIntResult(Query query)
        {
            var sql = Compiler.Compile(query);

            var result = await Connection.QueryFirstOrDefaultAsync<int?>(sql.Sql, sql.NamedBindings);
            return result;
        }

        protected async Task<string> ExecuteQueryStringResult(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QuerySingleOrDefaultAsync<string>(sql.Sql, sql.NamedBindings);
        }
    }
}
