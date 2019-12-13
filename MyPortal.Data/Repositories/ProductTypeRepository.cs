using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ProductTypeRepository : ReadWriteRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}