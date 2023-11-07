using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ICourseRepository : IReadWriteRepository<Course>, IUpdateRepository<Course>
    {
    }
}