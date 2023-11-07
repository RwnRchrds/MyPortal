using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IReportCardRepository : IReadWriteRepository<ReportCard>, IUpdateRepository<ReportCard>
    {
    }
}