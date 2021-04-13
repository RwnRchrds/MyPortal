using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ProductTypeRepository : BaseReadWriteRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(ProductType entity)
        {
            var productType = await Context.ProductTypes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (productType == null)
            {
                throw new EntityNotFoundException("Product type not found.");
            }

            productType.Description = entity.Description;
            productType.Active = entity.Active;
            productType.IsMeal = entity.IsMeal;
        }
    }
}