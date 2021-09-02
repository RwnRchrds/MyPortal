using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamBaseElementRepository : IReadWriteRepository<ExamBaseElement>, IUpdateRepository<ExamBaseElement>
    {
        
    }
}