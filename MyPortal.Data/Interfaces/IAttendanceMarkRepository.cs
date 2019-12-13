using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IAttendanceMarkRepository : IReadWriteRepository<AttendanceMark>
    {
        Task<AttendanceMark> Get(int studentId, int weekId, int periodId);

        Task<IEnumerable<AttendanceMark>> GetByStudent(int studentId, int academicYearId);
    }
}
