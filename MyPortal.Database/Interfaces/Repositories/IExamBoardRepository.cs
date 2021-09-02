using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamBoardRepository : IReadWriteRepository<ExamBoard>, IUpdateRepository<ExamBoard>
    {
        
    }
}