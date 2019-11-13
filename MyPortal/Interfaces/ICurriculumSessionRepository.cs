using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface ICurriculumSessionRepository : IRepository<CurriculumSession>
    {
        Task<CurriculumSession> GetByIdWithRelated(int sessionId);

        Task<IEnumerable<CurriculumSession>> GetByDayOfWeek(int academicYearId, int staffId, DayOfWeek dayOfWeek);

        Task<IEnumerable<CurriculumSession>> GetByClass(int classId);
    }
}
