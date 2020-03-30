using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class ProductTypeRepository : BaseReadWriteRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }

        protected override async Task<IEnumerable<ProductType>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<ProductType>(sql, param);
        }
    }
}