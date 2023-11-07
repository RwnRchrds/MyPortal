using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAchievementRepository : IReadWriteRepository<Achievement>, IUpdateRepository<Achievement>
    {
    }
}