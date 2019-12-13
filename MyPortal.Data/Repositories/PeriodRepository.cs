using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class PeriodRepository : ReadWriteRepository<Period>, IPeriodRepository
    {
        public PeriodRepository(MyPortalDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Period>> GetAllPeriods()
        {
            return await Context.Periods.OrderBy(x => x.Weekday).ThenBy(x => x.StartTime).ToListAsync();
        }

        public async Task<IEnumerable<Period>> GetByDayOfWeek(DayOfWeek weekDay)
        {
            return await Context.Periods.Where(x => x.Weekday == weekDay).OrderBy(x => x.StartTime).ToListAsync();
        }

        public async Task<IEnumerable<Period>> GetByClass(int classId)
        {
            return await Context.Periods.Where(x => x.Sessions.Any(s => s.ClassId == classId)).ToListAsync();
        }

        public async Task<IEnumerable<Period>> GetRegPeriods()
        {
            return await Context.Periods.Where(x => x.IsAm || x.IsPm).ToListAsync();
        }
    }
}