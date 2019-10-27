using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class AttendanceMarkRepository : Repository<AttendanceMark>, IAttendanceMarkRepository
    {
        public AttendanceMarkRepository(MyPortalDbContext context) : base(context)
        {

        }

        public Task<AttendanceMark> GetAttendanceMark(int studentId, int weekId, int periodId)
        {
            return Context.AttendanceMarks.SingleOrDefaultAsync(x =>
                x.StudentId == studentId && x.WeekId == weekId && x.PeriodId == periodId);
        }

        public async Task<IEnumerable<AttendanceMark>> GetAllAttendanceMarksByStudent(int studentId, int academicYearId)
        {
            return await Context.AttendanceMarks
                .Where(x => x.StudentId == studentId && x.Week.AcademicYearId == academicYearId).ToListAsync();
        }
    }
}