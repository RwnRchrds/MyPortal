using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ProductDiscountRepository : BaseReadWriteRepository<ProductDiscount>, IProductDiscountRepository
    {
        public ProductDiscountRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "Products", "P", "ProductId");
            JoinEntity(query, "StoreDiscounts", "SD", "StoreDiscountId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Product), "P");
            query.SelectAllColumns(typeof(StoreDiscount), "SD");

            return query;
        }

        protected override async Task<IEnumerable<ProductDiscount>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var discounts =
                await Transaction.Connection.QueryAsync<ProductDiscount, Product, StoreDiscount, ProductDiscount>(
                    sql.Sql,
                    (pd, product, discount) =>
                    {
                        pd.Product = product;
                        pd.StoreDiscount = discount;

                        return pd;
                    }, sql.NamedBindings, Transaction);

            return discounts;
        }
    }
}