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
    public class FinanceBasketItemRepository : ReadWriteRepository<FinanceBasketItem>, IFinanceBasketItemRepository
    {
        public FinanceBasketItemRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<FinanceBasketItem>> GetByStudent(int studentId)
        {
            return await Context.FinanceBasketItems.Where(x => x.StudentId == studentId).ToListAsync();
        }

        public async Task<decimal> GetTotalForStudent(int studentId)
        {
            return await Context.FinanceBasketItems.Where(x => x.StudentId == studentId)
                       .SumAsync(x => (decimal?) x.Product.Price) ?? 0;
        }
    }
}