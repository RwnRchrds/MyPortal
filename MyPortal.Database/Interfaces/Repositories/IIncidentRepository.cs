using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IIncidentRepository : IReadWriteRepository<Incident>, IUpdateRepository<Incident>
    {
    }
}