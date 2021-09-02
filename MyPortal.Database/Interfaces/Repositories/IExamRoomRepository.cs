using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamRoomRepository : IReadWriteRepository<ExamRoom>, IUpdateRepository<ExamRoom>
    {
        
    }
}