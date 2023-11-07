using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IRoomRepository : IReadWriteRepository<Room>, IUpdateRepository<Room>
    {
    }
}