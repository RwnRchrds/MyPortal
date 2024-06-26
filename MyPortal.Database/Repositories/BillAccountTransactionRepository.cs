﻿using System.Collections.Generic;
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
    public class BillAccountTransactionRepository : BaseReadWriteRepository<BillAccountTransaction>,
        IBillAccountTransactionRepository
    {
        public BillAccountTransactionRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Bills as B", "B.Id", $"{TblAlias}.BillId");
            query.LeftJoin("AccountTransactions as AT", "AT.Id", $"{TblAlias}.AccountTransactionId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Bill), "B");
            query.SelectAllColumns(typeof(AccountTransaction), "AT");

            return query;
        }

        protected override async Task<IEnumerable<BillAccountTransaction>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var accountTransactions =
                await DbUser.Transaction.Connection
                    .QueryAsync<BillAccountTransaction, Bill, AccountTransaction, BillAccountTransaction>(sql.Sql,
                        (bat, bill, transaction) =>
                        {
                            bat.Bill = bill;
                            bat.AccountTransaction = transaction;

                            return bat;
                        }, sql.NamedBindings, DbUser.Transaction);

            return accountTransactions;
        }
    }
}