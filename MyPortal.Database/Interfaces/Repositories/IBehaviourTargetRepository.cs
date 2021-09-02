using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IBehaviourTargetRepository : IReadWriteRepository<BehaviourTarget>, IUpdateRepository<BehaviourTarget>
    {
        
    }
}