using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class AcademicYearRepository : ReadWriteRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<AcademicYear> GetCurrent()
        {
            return await Context.AcademicYears.SingleOrDefaultAsync(x =>
                       x.FirstDate >= DateTime.Today && x.LastDate <= DateTime.Today) ?? await Context
                       .AcademicYears.OrderByDescending(x => x.FirstDate).FirstOrDefaultAsync();
        }
    }
}