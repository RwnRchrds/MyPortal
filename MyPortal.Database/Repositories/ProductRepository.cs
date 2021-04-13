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
    public class ProductRepository : BaseReadWriteRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "Product")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ProductType), "ProductType");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("ProductTypes as ProductType", "ProductType.Id", "Product.ProductTypeId");
        }

        protected override async Task<IEnumerable<Product>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<Product, ProductType, Product>(sql.Sql, (product, type) =>
            {
                product.Type = type;

                return product;
            }, sql.NamedBindings, Transaction);
        }

        public async Task Update(Product entity)
        {
            var product = await Context.Products.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (product == null)
            {
                throw new EntityNotFoundException("Product not found.");
            }

            product.Name = entity.Name;
            product.Description = entity.Description;
            product.Price = entity.Price;
            product.OrderLimit = entity.OrderLimit;
            product.ProductTypeId = entity.ProductTypeId;
            product.VatRateId = entity.VatRateId;
            product.ShowOnStore = entity.ShowOnStore;
        }
    }
}