using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamSeriesRepository : IReadWriteRepository<ExamSeries>, IUpdateRepository<ExamSeries>
    {
    }
}