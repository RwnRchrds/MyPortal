using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamSpecialArrangementRepository : IReadWriteRepository<ExamSpecialArrangement>, IUpdateRepository<ExamSpecialArrangement>
    {
        
    }
}