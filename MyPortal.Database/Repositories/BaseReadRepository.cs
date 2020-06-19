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

            return await Connection.QueryAsync<TEntity>(sql.Sql, sql.Bindings);
        }

        protected virtual void JoinRelated(Query query)
        {
            
        }

        protected virtual void SelectAllRelated(Query query)
        {
            JoinRelated(query);
        }

        protected async Task<int> ExecuteIntQuery(Query query)
        {
            var compiled = Compiler.Compile(query);

            var result = await Connection.QueryFirstOrDefaultAsync<int?>(compiled.Sql, compiled.Bindings);

            if (result == null)
            {
                return 0;
            }

            return result.Value;
        }

        protected async Task<string> ExecuteStringQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QuerySingleOrDefaultAsync<string>(sql.Sql, sql.Bindings);
        }

        protected async Task<int> ExecuteNonQuery(Query query)
        {
            var compiled = Compiler.Compile(query);

            return await Connection.ExecuteAsync(compiled.Sql, compiled.Bindings);
        }

        protected Query SelectAllColumns(bool getRelated = true)
        {
            var query = new Query(TblName).SelectAll(typeof(TEntity));

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
