using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAchievementTypeRepository : IReadWriteRepository<AchievementType>
    {
        Task<IEnumerable<AchievementType>> GetRecorded(Guid academicYearId);
    }
}
