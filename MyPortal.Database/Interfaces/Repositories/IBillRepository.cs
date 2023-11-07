using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IBillRepository : IReadWriteRepository<Bill>, IUpdateRepository<Bill>
    {
    }
}