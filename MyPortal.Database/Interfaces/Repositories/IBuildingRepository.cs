using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IBuildingRepository : IReadWriteRepository<Building>, IUpdateRepository<Building>
    {
    }
}