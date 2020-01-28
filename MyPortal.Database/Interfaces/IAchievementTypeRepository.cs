using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IAchievementTypeRepository : IReadWriteRepository<AchievementType, int>
    {
        Task<IEnumerable<AchievementType>> GetRecorded(int academicYearId);
    }
}
