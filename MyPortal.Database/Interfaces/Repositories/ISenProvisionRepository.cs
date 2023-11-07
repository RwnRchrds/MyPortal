using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ISenProvisionRepository : IReadWriteRepository<SenProvision>, IUpdateRepository<SenProvision>
    {
    }
}