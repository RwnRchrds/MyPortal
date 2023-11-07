using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ILocationRepository : IReadWriteRepository<Location>, IUpdateRepository<Location>
    {
    }
}