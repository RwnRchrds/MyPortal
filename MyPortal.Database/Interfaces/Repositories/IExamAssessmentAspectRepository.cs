using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamAssessmentAspectRepository : IReadWriteRepository<ExamAssessmentAspect>,
        IUpdateRepository<ExamAssessmentAspect>
    {
    }
}