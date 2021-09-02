using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamCandidateSeriesRepository : IReadWriteRepository<ExamCandidateSeries>, IUpdateRepository<ExamCandidateSeries>
    {
        
    }
}