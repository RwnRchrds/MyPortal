using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Extensions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Services;

namespace MyPortal.Repositories
{
    public class AttendanceWeekRepository : Repository<AttendanceWeek>, IAttendanceWeekRepository
    {
        public AttendanceWeekRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<AttendanceWeek> GetByDate(int academicYearId, DateTime date)
        {
            var weekBeginning = date.StartOfWeek();

            return await Context.AttendanceWeeks.SingleOrDefaultAsync(x =>
                x.AcademicYearId == academicYearId && x.Beginning == weekBeginning);
        }
    }
}