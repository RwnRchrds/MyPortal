using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        protected abstract Task<IEnumerable<TEntity>> ExecuteQuery(string sql, object param = null);

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
