using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamSeasonRepository : IReadWriteRepository<ExamSeason>, IUpdateRepository<ExamSeason>
    {
    }
}