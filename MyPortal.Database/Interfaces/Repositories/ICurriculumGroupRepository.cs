using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ICurriculumGroupRepository : IReadWriteRepository<CurriculumGroup>,
        IUpdateRepository<CurriculumGroup>
    {
    }
}