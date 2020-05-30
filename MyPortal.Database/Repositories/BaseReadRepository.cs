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
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public abstract class BaseReadRepository<TEntity> : IDisposable where TEntity : class
    {
        protected readonly IDbConnection Connection;

        public BaseReadRepository(IDbConnection connection, string tblAlias = null)
        {
            Connection = connection;

            TblAlias = tblAlias ?? typeof(TEntity).Name;

            TblName = EntityHelper.GetTblName(typeof(TEntity), TblAlias, "dbo", true);

            AllColumns = EntityHelper.GetAllColumns(typeof(TEntity), TblAlias);
        }

        protected string TblAlias;
        
        protected string TblName;

        protected string AllColumns;

        protected string RelatedColumns = null;

        protected string JoinRelated = null;
        
        protected bool HasRelated => RelatedColumns != null && JoinRelated != null;

        protected abstract Task<IEnumerable<TEntity>> ExecuteQuery(string sql, object param = null);

        protected async Task<int> ExecuteIntQuery(string sql, object param = null)
        {
            var result = await Connection.QueryFirstOrDefaultAsync<int?>(sql, param);

            if (result == null)
            {
                return 0;
            }

            return result.Value;
        }

        protected async Task<string> ExecuteStringQuery(string sql, object param = null)
        {
            return await Connection.QuerySingleOrDefaultAsync<string>(sql, param);
        }

        protected async Task<int> ExecuteNonQuery(string sql, object param = null)
        {
            return await Connection.ExecuteAsync(sql, param);
        }

        protected string SelectAllColumns(bool getRelated = true)
        {
            if (getRelated && HasRelated)
            {
                return $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";
            }

            return $"SELECT {AllColumns} FROM {TblName}";
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var sql = SelectAllColumns(true);

            return await ExecuteQuery(sql);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var sql = SelectAllColumns(true);
            
            SqlHelper.Where(ref sql, $"[{TblAlias}].[Id] = @Id");

            return (await ExecuteQuery(sql, new {Id = id})).SingleOrDefault();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
