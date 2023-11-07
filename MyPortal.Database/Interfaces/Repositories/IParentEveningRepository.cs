using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IParentEveningRepository : IReadWriteRepository<ParentEvening>, IUpdateRepository<ParentEvening>
    {
    }
}