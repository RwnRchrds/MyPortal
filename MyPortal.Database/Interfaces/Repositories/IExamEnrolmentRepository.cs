using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamEnrolmentRepository : IReadWriteRepository<ExamEnrolment>, IUpdateRepository<ExamEnrolment>
    {
        
    }
}