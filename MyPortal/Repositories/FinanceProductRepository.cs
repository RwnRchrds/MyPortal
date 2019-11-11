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
    public class FinanceProductRepository : Repository<FinanceProduct>, IFinanceProductRepository
    {
        public FinanceProductRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<FinanceProduct>> GetAvailableByStudent(int studentId)
        {
            return await Context.FinanceProducts
                .Where(x => !x.Deleted && x.Visible &&
                            (!x.OnceOnly || x.OnceOnly && x.BasketItems.All(b => b.StudentId != studentId) &&
                             x.Sales.All(s => s.StudentId != studentId))).OrderBy(x => x.Description).ToListAsync();
        }
    }
}