using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AccountTransactionRepository : BaseReadWriteRepository<AccountTransaction>, IAccountTransactionRepository
    {
        public AccountTransactionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "S");

            return query;
        }

        protected override async Task<IEnumerable<AccountTransaction>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var transactions = await Transaction.Connection.QueryAsync<AccountTransaction, Student, AccountTransaction>(sql.Sql,
                (transaction, student) =>
                {
                    transaction.Student = student;

                    return transaction;
                }, sql.NamedBindings, Transaction);

            return transactions;
        }
    }
}
