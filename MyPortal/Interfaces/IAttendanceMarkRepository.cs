using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IAttendanceMarkRepository : IRepository<AttendanceMark>
    {
        Task<AttendanceMark> Get(int studentId, int weekId, int periodId);

        Task<IEnumerable<AttendanceMark>> GetByStudent(int studentId, int academicYearId);
    }
}
