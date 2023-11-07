using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IHomeworkItemRepository : IReadWriteRepository<HomeworkItem>, IUpdateRepository<HomeworkItem>
    {
        Task<IEnumerable<HomeworkItem>> GetHomework(HomeworkSearchOptions searchOptions);
    }
}