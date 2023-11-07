using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IGradeSetRepository : IReadWriteRepository<GradeSet>, IUpdateRepository<GradeSet>
    {
    }
}