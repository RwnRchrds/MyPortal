using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ProductTypeRepository : BaseReadWriteRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }

        protected override async Task<IEnumerable<ProductType>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<ProductType>(sql, param);
        }

        public async Task Update(ProductType entity)
        {
            var productType = await Context.ProductTypes.FindAsync(entity.Id);

            productType.Description = entity.Description;
            productType.IsMeal = entity.IsMeal;
        }
    }
}