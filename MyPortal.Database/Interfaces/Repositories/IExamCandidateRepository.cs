using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamCandidateRepository : IReadWriteRepository<ExamCandidate>, IUpdateRepository<ExamCandidate>
    {
        
    }
}