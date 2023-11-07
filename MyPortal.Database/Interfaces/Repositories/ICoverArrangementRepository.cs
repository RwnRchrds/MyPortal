using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ICoverArrangementRepository : IReadWriteRepository<CoverArrangement>,
        IUpdateRepository<CoverArrangement>
    {
    }
}