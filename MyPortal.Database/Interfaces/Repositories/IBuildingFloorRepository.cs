using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IBuildingFloorRepository : IReadWriteRepository<BuildingFloor>, IUpdateRepository<BuildingFloor>
    {
    }
}