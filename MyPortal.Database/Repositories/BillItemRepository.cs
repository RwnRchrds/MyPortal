using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class BillItemRepository : BaseReadWriteRepository<BillItem>, IBillItemRepository
    {
        public BillItemRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Bills", "B", "BillId");
            JoinEntity(query, "Products", "P", "ProductId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Bill), "B");
            query.SelectAllColumns(typeof(Product), "P");

            return query;
        }

        protected override async Task<IEnumerable<BillItem>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var billItems = await Transaction.Connection.QueryAsync<BillItem, Bill, Product, BillItem>(sql.Sql,
                (item, bill, product) =>
                {
                    item.Bill = bill;
                    item.Product = product;

                    return item;
                }, sql.NamedBindings, Transaction);

            return billItems;
        }
    }
}