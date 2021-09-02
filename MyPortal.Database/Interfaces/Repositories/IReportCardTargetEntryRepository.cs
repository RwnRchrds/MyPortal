using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IReportCardTargetEntryRepository : IReadWriteRepository<ReportCardTargetEntry>, IUpdateRepository<ReportCardTargetEntry>
    {
        
    }
}