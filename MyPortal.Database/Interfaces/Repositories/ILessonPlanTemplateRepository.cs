using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ILessonPlanTemplateRepository : IReadWriteRepository<LessonPlanTemplate>,
        IUpdateRepository<LessonPlanTemplate>
    {
    }
}