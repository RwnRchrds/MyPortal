using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class BehaviourAchievementTypeRepository : ReadOnlyRepository<BehaviourAchievementType>, IBehaviourAchievementTypeRepository
    {
        public BehaviourAchievementTypeRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<BehaviourAchievementType>> GetAllRecordedAchievementTypes(int academicYearId)
        {
            return await Context.BehaviourAchievementTypes
                .Where(x => x.Achievements.Any(a => a.AcademicYearId == academicYearId)).ToListAsync();
        }
    }
}