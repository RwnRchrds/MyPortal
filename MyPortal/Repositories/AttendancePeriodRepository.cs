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
    public class AttendancePeriodRepository : Repository<AttendancePeriod>, IAttendancePeriodRepository
    {
        public AttendancePeriodRepository(MyPortalDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<AttendancePeriod>> GetAllPeriods()
        {
            return await Context.AttendancePeriods.OrderBy(x => x.Weekday).ThenBy(x => x.StartTime).ToListAsync();
        }

        public async Task<IEnumerable<AttendancePeriod>> GetByDayOfWeek(DayOfWeek weekDay)
        {
            return await Context.AttendancePeriods.Where(x => x.Weekday == weekDay).ToListAsync();
        }

        public async Task<IEnumerable<AttendancePeriod>> GetByClass(int classId)
        {
            return await Context.AttendancePeriods.Where(x => x.Sessions.Any(s => s.ClassId == classId)).ToListAsync();
        }

        public async Task<IEnumerable<AttendancePeriod>> GetRegPeriods()
        {
            return await Context.AttendancePeriods.Where(x => x.IsAm || x.IsPm).ToListAsync();
        }
    }
}