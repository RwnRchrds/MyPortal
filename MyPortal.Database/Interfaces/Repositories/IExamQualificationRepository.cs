using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamQualificationRepository : IReadWriteRepository<ExamQualification>,
        IUpdateRepository<ExamQualification>
    {
    }
}