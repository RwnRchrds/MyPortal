using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class LessonPlanTemplateRepository : BaseReadWriteRepository<LessonPlanTemplate>, ILessonPlanTemplateRepository
    {
        public LessonPlanTemplateRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(LessonPlanTemplate entity)
        {
            var template = await Context.LessonPlanTemplates.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (template == null)
            {
                throw new EntityNotFoundException("Lesson plan template not found.");
            }

            template.Name = entity.Name;
            template.PlanTemplate = entity.PlanTemplate;
        }
    }
}