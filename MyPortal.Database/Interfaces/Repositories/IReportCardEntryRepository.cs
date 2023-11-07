using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IReportCardEntryRepository : IReadWriteRepository<ReportCardEntry>,
        IUpdateRepository<ReportCardEntry>
    {
    }
}