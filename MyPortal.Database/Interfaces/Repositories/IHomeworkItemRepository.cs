using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IHomeworkItemRepository : IReadWriteRepository<HomeworkItem>, IUpdateRepository<HomeworkItem>
    {

    }
}
