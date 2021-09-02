using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAgencyRepository : IReadWriteRepository<Agency>, IUpdateRepository<Agency>
    {
        
    }
}