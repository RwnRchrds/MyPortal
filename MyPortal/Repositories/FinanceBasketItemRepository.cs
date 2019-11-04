using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class FinanceBasketItemRepository : Repository<FinanceBasketItem>, IFinanceBasketItemRepository
    {
        public FinanceBasketItemRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<FinanceBasketItem>> GetBasketItemsByStudent(int studentId)
        {
            return await Context.FinanceBasketItems.Where(x => x.StudentId == studentId).ToListAsync();
        }

        public async Task<decimal> GetBasketTotalForStudent(int studentId)
        {
            return await Context.FinanceBasketItems.Where(x => x.StudentId == studentId)
                       .SumAsync(x => (decimal?) x.Product.Price) ?? 0;
        }
    }
}