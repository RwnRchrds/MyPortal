using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ProductRepository : ReadWriteRepository<Product>, IProductRepository
    {
        public ProductRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>> GetAvailableByStudent(int studentId)
        {
            return await Context.Products
                .Where(x => !x.Deleted && x.Visible &&
                            (!x.OnceOnly || x.OnceOnly && x.BasketItems.All(b => b.StudentId != studentId) &&
                             x.Sales.All(s => s.StudentId != studentId))).OrderBy(x => x.Description).ToListAsync();
        }
    }
}