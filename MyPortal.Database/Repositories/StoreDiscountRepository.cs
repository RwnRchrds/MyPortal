using System.Collections.Generic;
using System.Data.Common;
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
    public class StoreDiscountRepository : BaseReadWriteRepository<StoreDiscount>, IStoreDiscountRepository
    {
        public StoreDiscountRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Products as P", "P.Id", $"{TblAlias}.ProductId");
            query.LeftJoin("ProductTypes as PT", "PT.Id", $"{TblAlias}.ProductTypeId");
            query.LeftJoin("Discounts as D", "D.Id", $"{TblAlias}.DiscountId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Product), "P");
            query.SelectAllColumns(typeof(ProductType), "PT");
            query.SelectAllColumns(typeof(Discount), "D");

            return query;
        }

        protected override async Task<IEnumerable<StoreDiscount>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var discounts =
                await Transaction.Connection.QueryAsync<StoreDiscount, Product, ProductType, Discount, StoreDiscount>(
                    sql.Sql,
                    (pd, product, productType, discount) =>
                    {
                        pd.Product = product;
                        pd.ProductType = productType;
                        pd.Discount = discount;

                        return pd;
                    }, sql.NamedBindings, Transaction);

            return discounts;
        }

        public async Task Update(StoreDiscount entity)
        {
            var storeDiscount = await Context.StoreDiscounts.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (storeDiscount == null)
            {
                throw new EntityNotFoundException("Store discount not found.");
            }

            storeDiscount.ApplyTo = entity.ApplyTo;
            storeDiscount.ApplyToCart = entity.ApplyToCart;
            storeDiscount.ProductId = entity.ProductId;
            storeDiscount.ProductTypeId = entity.ProductTypeId;
        }
    }
}