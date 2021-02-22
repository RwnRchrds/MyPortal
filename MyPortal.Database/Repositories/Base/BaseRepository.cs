using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SqlKata.Compilers;

namespace MyPortal.Database.Repositories.Base
{
    public class BaseRepository : IDisposable
    {
        protected IDbConnection Connection;
        protected readonly SqlServerCompiler Compiler;

        public BaseRepository(IDbConnection connection)
        {
            Connection = connection;
            Compiler = new SqlServerCompiler();
        }

        public virtual void Dispose()
        {
            Connection.Dispose();
        }
    }
}
