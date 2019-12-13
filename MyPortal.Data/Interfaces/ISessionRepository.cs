using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface ISessionRepository : IReadWriteRepository<Session>
    {
        Task<Session> GetByIdWithRelated(int sessionId, params Expression<Func<Session, object>>[] includeProperties);

        Task<IEnumerable<Session>> GetByDayOfWeek(int academicYearId, int staffId, DayOfWeek dayOfWeek);

        Task<IEnumerable<Session>> GetByClass(int classId);
    }
}
