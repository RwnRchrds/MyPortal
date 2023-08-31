using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AccountTransactionRepository : BaseReadWriteRepository<AccountTransaction>, IAccountTransactionRepository
    {
        public AccountTransactionRepository(DbUserWithContext dbUser) : base(dbUser)
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

            var transactions = await DbUser.Transaction.Connection.QueryAsync<AccountTransaction, Student, AccountTransaction>(sql.Sql,
                (transaction, student) =>
                {
                    transaction.Student = student;

                    return transaction;
                }, sql.NamedBindings, DbUser.Transaction);

            return transactions;
        }
    }
}
