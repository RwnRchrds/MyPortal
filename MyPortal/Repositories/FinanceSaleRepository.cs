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
    public class FinanceSaleRepository : Repository<FinanceSale>, IFinanceSaleRepository
    {
        public FinanceSaleRepository(MyPortalDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<FinanceSale>> GetAllAsync(int academicYearId)
        {
            return await Context.FinanceSales.Where(x => !x.Deleted && x.AcademicYearId == academicYearId).OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<FinanceSale>> GetPending(int academicYearId)
        {
            return await Context.FinanceSales
                .Where(x => x.AcademicYearId == academicYearId && !x.Deleted && !x.Processed && !x.Refunded)
                .OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<IEnumerable<FinanceSale>> GetProcessed(int academicYearId)
        {
            return await Context.FinanceSales
                .Where(x => x.AcademicYearId == academicYearId && !x.Deleted && x.Processed)
                .OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<IEnumerable<FinanceSale>> GetByStudent(int studentId, int academicYearId)
        {
            return await Context.FinanceSales.Where(x => x.StudentId == studentId && x.AcademicYearId == academicYearId)
                .OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}