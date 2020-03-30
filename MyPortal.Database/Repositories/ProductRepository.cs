using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

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
    }
}