using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SaleRepository : ReadWriteRepository<Sale>, ISaleRepository
    {
        public SaleRepository(MyPortalDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Sale>> GetAllAsync(int academicYearId)
        {
            return await Context.Sales.Where(x => !x.Deleted && x.AcademicYearId == academicYearId).OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetPending(int academicYearId)
        {
            return await Context.Sales
                .Where(x => x.AcademicYearId == academicYearId && !x.Deleted && !x.Processed && !x.Refunded)
                .OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetProcessed(int academicYearId)
        {
            return await Context.Sales
                .Where(x => x.AcademicYearId == academicYearId && !x.Deleted && x.Processed)
                .OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetByStudent(int studentId, int academicYearId)
        {
            return await Context.Sales.Where(x => x.StudentId == studentId && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}