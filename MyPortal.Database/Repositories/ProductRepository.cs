using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ProductRepository : BaseReadWriteRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"{EntityHelper.GetAllColumns(typeof(ProductType))}";

            JoinRelated =
                $@"{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[ProductType]", "[ProductType].[Id]", "[Product].[ProductTypeId]")}";
        }

        protected override async Task<IEnumerable<Product>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Product, ProductType, Product>(sql, (product, type) =>
            {
                product.Type = type;

                return product;
            }, param);
        }

        public async Task Update(Product entity)
        {
            var product = await Context.Products.FindAsync(entity.Id);

            product.Description = entity.Description;
            product.ProductTypeId = entity.ProductTypeId;
            product.Price = entity.Price;
            product.Visible = entity.Visible;
            product.OnceOnly = entity.OnceOnly;
            product.Deleted = entity.Deleted;
        }
    }
}