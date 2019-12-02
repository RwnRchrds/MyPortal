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
    public class CurriculumLessonPlanRepository : ReadWriteRepository<CurriculumLessonPlan>, ICurriculumLessonPlanRepository
    {
        public CurriculumLessonPlanRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CurriculumLessonPlan>> GetByStudyTopic(int studyTopicId)
        {
            return await Context.CurriculumLessonPlans.Where(x => x.StudyTopicId == studyTopicId).ToListAsync();
        }
    }
}