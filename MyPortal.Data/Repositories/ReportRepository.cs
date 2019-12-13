using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ReportRepository : ReadRepository<Report>, IReportRepository
    {
        public ReportRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}