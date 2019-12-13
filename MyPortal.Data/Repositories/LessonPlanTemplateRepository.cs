using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class LessonPlanTemplateRepository : ReadWriteRepository<LessonPlanTemplate>, ILessonPlanTemplateRepository
    {
        public LessonPlanTemplateRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}