using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;
using SqlKata.Compilers;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public abstract class BaseReadRepository<TEntity> : IDisposable where TEntity : class
    {
        protected readonly IDbConnection Connection;
        protected readonly SqlServerCompiler Compiler;

        public BaseReadRepository(IDbConnection connection, string tblAlias = null)
        {
            Connection = connection;

            Compiler = new SqlServerCompiler();

            TblAlias = string.IsNullOrWhiteSpace(tblAlias) ? typeof(TEntity).Name : tblAlias;

            TblName = EntityHelper.GetTableName(typeof(TEntity), tblAlias);
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

        protected virtual void JoinRelated(Query query)
        {
            
        }

        protected virtual void SelectAllRelated(Query query)
        {
            JoinRelated(query);
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

        protected async Task<int> ExecuteNonQuery(Query query)
        {
            var compiled = Compiler.Compile(query);

            return await Connection.ExecuteAsync(compiled.Sql, compiled.Bindings);
        }

        protected Query SelectAllColumns(bool includeDeleted = false, bool getRelated = true)
        {
            var query = new Query(TblName).SelectAll(typeof(TEntity));

            if (!includeDeleted && typeof(TEntity).GetInterfaces().Contains(typeof(ISoftDelete)))
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
            var sql = SelectAllColumns();

            return await ExecuteQuery(sql);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var query = SelectAllColumns();

            query.Where($"{TblAlias}.Id", "=", id);

            return (await ExecuteQuery(query)).SingleOrDefault();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
