using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamQualificationLevelRepository : IReadWriteRepository<ExamQualificationLevel>,
        IUpdateRepository<ExamQualificationLevel>
    {
    }
}