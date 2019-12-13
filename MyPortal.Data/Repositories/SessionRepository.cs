using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyPortal.Data.Extensions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SessionRepository : ReadWriteRepository<Session>, ISessionRepository
    {
        public SessionRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<Session> GetByIdWithRelated(int sessionId, params Expression<Func<Session, object>>[] includeProperties)
        {
            return await Context.Sessions.IncludeMultiple(includeProperties).SingleOrDefaultAsync(x => x.Id == sessionId);
        }

        public async Task<IEnumerable<Session>> GetByClass(int classId)
        {
            return await Context.Sessions.Where(x => x.ClassId == classId).OrderBy(x => x.Period.Weekday).ThenBy(x => x.Period.StartTime).ToListAsync();
        }

        public async Task<IEnumerable<Session>> GetByDayOfWeek(int academicYearId, int staffId, DayOfWeek dayOfWeek)
        {
            return await Context.Sessions.Where(x =>
                x.Class.TeacherId == staffId && x.Class.AcademicYearId == academicYearId &&
                x.Period.Weekday == dayOfWeek).ToListAsync();
        }
    }
}