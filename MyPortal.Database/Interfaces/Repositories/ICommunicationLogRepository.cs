using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ICommunicationLogRepository : IReadWriteRepository<CommunicationLog>,
        IUpdateRepository<CommunicationLog>
    {
    }
}