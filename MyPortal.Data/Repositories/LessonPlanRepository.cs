using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class LessonPlanRepository : ReadWriteRepository<LessonPlan>, ILessonPlanRepository
    {
        public LessonPlanRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<LessonPlan>> GetByStudyTopic(int studyTopicId)
        {
            return await Context.LessonPlans.Where(x => x.StudyTopicId == studyTopicId).ToListAsync();
        }
    }
}