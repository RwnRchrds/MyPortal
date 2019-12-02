using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Extensions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CurriculumSessionRepository : ReadWriteRepository<CurriculumSession>, ICurriculumSessionRepository
    {
        public CurriculumSessionRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<CurriculumSession> GetByIdWithRelated(int sessionId, params Expression<Func<CurriculumSession, object>>[] includeProperties)
        {
            return await Context.CurriculumSessions.IncludeMultiple(includeProperties).SingleOrDefaultAsync(x => x.Id == sessionId);
        }

        public async Task<IEnumerable<CurriculumSession>> GetByClass(int classId)
        {
            return await Context.CurriculumSessions.Where(x => x.ClassId == classId).OrderBy(x => x.Period.Weekday).ThenBy(x => x.Period.StartTime).ToListAsync();
        }

        public async Task<IEnumerable<CurriculumSession>> GetByDayOfWeek(int academicYearId, int staffId, DayOfWeek dayOfWeek)
        {
            return await Context.CurriculumSessions.Where(x =>
                x.Class.TeacherId == staffId && x.Class.AcademicYearId == academicYearId &&
                x.Period.Weekday == dayOfWeek).ToListAsync();
        }
    }
}