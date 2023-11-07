using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamElementRepository : IReadWriteRepository<ExamElement>, IUpdateRepository<ExamElement>
    {
    }
}