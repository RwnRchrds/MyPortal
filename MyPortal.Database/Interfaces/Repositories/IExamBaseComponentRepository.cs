using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamBaseComponentRepository : IReadWriteRepository<ExamBaseComponent>, IUpdateRepository<ExamBaseComponent>
    {
        
    }
}