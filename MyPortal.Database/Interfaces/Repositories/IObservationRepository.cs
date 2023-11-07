using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IObservationRepository : IReadWriteRepository<Observation>, IUpdateRepository<Observation>
    {
    }
}