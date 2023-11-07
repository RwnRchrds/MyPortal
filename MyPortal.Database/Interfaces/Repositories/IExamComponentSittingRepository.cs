using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamComponentSittingRepository : IReadWriteRepository<ExamComponentSitting>,
        IUpdateRepository<ExamComponentSitting>
    {
    }
}