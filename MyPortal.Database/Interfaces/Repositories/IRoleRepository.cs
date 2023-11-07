using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IRoleRepository : IReadWriteRepository<Role>, IUpdateRepository<Role>
    {
    }
}