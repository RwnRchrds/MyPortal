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
    public class BillStoreDiscountRepository : BaseReadWriteRepository<BillStoreDiscount>, IBillStoreDiscountRepository
    {
        public BillStoreDiscountRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "StoreDiscounts", "SD", "StoreDiscountId");
            JoinEntity(query, "Bills", "B", "BillId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StoreDiscount), "SD");
            query.SelectAllColumns(typeof(Bill), "B");

            return query;
        }

        protected override async Task<IEnumerable<BillStoreDiscount>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var billStoreDiscounts =
                await Transaction.Connection.QueryAsync<BillStoreDiscount, StoreDiscount, Bill, BillStoreDiscount>(
                    sql.Sql,
                    (bsd, storeDiscount, bill) =>
                    {
                        bsd.StoreDiscount = storeDiscount;
                        bsd.Bill = bill;

                        return bsd;
                    }, sql.NamedBindings, Transaction);

            return billStoreDiscounts;
        }
    }
}