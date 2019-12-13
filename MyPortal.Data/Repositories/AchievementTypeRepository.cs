using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class AchievementTypeRepository : ReadRepository<AchievementType>, IAchievementTypeRepository
    {
        public AchievementTypeRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<AchievementType>> GetRecorded(int academicYearId)
        {
            return await Context.AchievementTypes
                .Where(x => x.Achievements.Any(a => a.AcademicYearId == academicYearId)).ToListAsync();
        }
    }
}