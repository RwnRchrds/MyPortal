using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamResultEmbargoRepository : IReadWriteRepository<ExamResultEmbargo>, IUpdateRepository<ExamResultEmbargo>
    {
        
    }
}