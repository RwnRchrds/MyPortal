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
        Task<IEnumerable<CurriculumSession>> GetSessionsByDate(int academicYearId, int staffId, DateTime date);

        Task<IEnumerable<CurriculumSession>> GetSessionsByClass(int classId);
    }
}
