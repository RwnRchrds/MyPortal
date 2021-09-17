using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamRoomSeatBlockRepository : IReadWriteRepository<ExamRoomSeatBlock>, IUpdateRepository<ExamRoomSeatBlock>
    {
        
    }
}