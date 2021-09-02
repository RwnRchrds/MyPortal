using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamAwardRepository : IReadWriteRepository<ExamAward>, IUpdateRepository<ExamAward>
    {
        
    }
}