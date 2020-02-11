using System;
using System.Collections.Generic;
using System.Data;
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

        public BaseReadRepository(IDbConnection connection)
        {
            Connection = connection;
        }

        protected readonly string TblName = EntityHelper.GetTblName(typeof(TEntity));

        protected readonly string AllColumns = EntityHelper.GetAllColumns(typeof(TEntity));

        protected abstract Task<IEnumerable<TEntity>> ExecuteQuery(string sql, object param = null);

        protected async Task<int> ExecuteIntQuery(string sql, object param = null)
        {
            return await Connection.QueryFirstAsync<int>(sql, param);
        }

        protected async Task<string> ExecuteStringQuery(string sql, object param = null)
        {
            return await Connection.QueryFirstAsync<string>(sql, param);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
