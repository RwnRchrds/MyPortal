using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamSeatAllocationRepository : IReadWriteRepository<ExamSeatAllocation>, IUpdateRepository<ExamSeatAllocation>
    {
        
    }
}