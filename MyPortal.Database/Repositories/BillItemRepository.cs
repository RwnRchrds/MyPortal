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
    public class BillItemRepository : BaseReadWriteRepository<BillItem>, IBillItemRepository
    {
        public BillItemRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Bills as B", "B.Id", $"{TblAlias}.BillId");
            query.LeftJoin("Products as P", "P.Id", $"{TblAlias}.ProductId");

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

            var billItems = await DbUser.Transaction.Connection.QueryAsync<BillItem, Bill, Product, BillItem>(sql.Sql,
                (item, bill, product) =>
                {
                    item.Bill = bill;
                    item.Product = product;

                    return item;
                }, sql.NamedBindings, DbUser.Transaction);

            return billItems;
        }
    }
}