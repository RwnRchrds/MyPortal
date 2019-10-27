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
        Task<AttendanceMark> GetAttendanceMark(int studentId, int weekId, int periodId);

        Task<IEnumerable<AttendanceMark>> GetAllAttendanceMarksByStudent(int studentId, int academicYearId);
    }
}
