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
        Task<IEnumerable<CurriculumSession>> GetSessionsByDayOfWeek(int academicYearId, int staffId, DayOfWeek dayOfWeek);

        Task<IEnumerable<CurriculumSession>> GetSessionsByClass(int classId);
    }
}
