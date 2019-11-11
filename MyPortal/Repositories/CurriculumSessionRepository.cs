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
    public class CurriculumSessionRepository : Repository<CurriculumSession>, ICurriculumSessionRepository
    {
        public CurriculumSessionRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CurriculumSession>> GetByClass(int classId)
        {
            return await Context.CurriculumSessions.Where(x => x.ClassId == classId).ToListAsync();
        }

        public async Task<IEnumerable<CurriculumSession>> GetByDayOfWeek(int academicYearId, int staffId, DayOfWeek dayOfWeek)
        {
            return await Context.CurriculumSessions.Where(x =>
                x.Class.TeacherId == staffId && x.Class.AcademicYearId == academicYearId &&
                x.Period.Weekday == dayOfWeek).ToListAsync();
        }
    }
}