using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class BasketItemRepository : ReadWriteRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<BasketItem>> GetByStudent(int studentId)
        {
            return await Context.BasketItems.Where(x => x.StudentId == studentId).ToListAsync();
        }

        public async Task<decimal> GetTotalForStudent(int studentId)
        {
            return await Context.BasketItems.Where(x => x.StudentId == studentId)
                       .SumAsync(x => (decimal?) x.Product.Price) ?? 0;
        }
    }
}