using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamAssessmentRepository : IReadWriteRepository<ExamAssessment>, IUpdateRepository<ExamAssessment>
    {
    }
}