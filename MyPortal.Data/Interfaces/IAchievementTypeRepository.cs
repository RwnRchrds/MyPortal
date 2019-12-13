using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IAchievementTypeRepository : IReadRepository<AchievementType>
    {
        Task<IEnumerable<AchievementType>> GetRecorded(int academicYearId);
    }
}
