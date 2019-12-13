using System;
using System.Data.Entity;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class AttendanceWeekRepository : ReadWriteRepository<AttendanceWeek>, IAttendanceWeekRepository
    {
        public AttendanceWeekRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<AttendanceWeek> GetByWeekBeginning(int academicYearId, DateTime weekBeginning)
        {
            return await Context.AttendanceWeeks.SingleOrDefaultAsync(x =>
                x.AcademicYearId == academicYearId && x.Beginning == weekBeginning);
        }
    }
}