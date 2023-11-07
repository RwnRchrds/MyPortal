using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IVatRateRepository : IReadWriteRepository<VatRate>, IUpdateRepository<VatRate>
    {
    }
}