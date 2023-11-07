using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAttendanceWeekPatternRepository : IReadWriteRepository<AttendanceWeekPattern>,
        IUpdateRepository<AttendanceWeekPattern>
    {
    }
}