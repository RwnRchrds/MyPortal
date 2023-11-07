using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IChargeRepository : IReadWriteRepository<Charge>, IUpdateRepository<Charge>
    {
    }
}