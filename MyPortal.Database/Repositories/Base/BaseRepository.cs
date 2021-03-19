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
        protected DbTransaction Transaction;
        protected readonly SqlServerCompiler Compiler;

        public BaseRepository(DbTransaction transaction)
        {
            Transaction = transaction;
            Compiler = new SqlServerCompiler();
        }
    }
}
