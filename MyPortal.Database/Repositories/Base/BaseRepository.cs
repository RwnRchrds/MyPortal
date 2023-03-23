using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using SqlKata.Compilers;

namespace MyPortal.Database.Repositories.Base
{
    public class BaseRepository
    {
        protected IDbTransaction Transaction;
        protected readonly SqlServerCompiler Compiler;

        public BaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
            Compiler = new SqlServerCompiler();
        }
    }
}
