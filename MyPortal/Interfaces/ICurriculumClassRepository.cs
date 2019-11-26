using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface ICurriculumClassRepository : IRepository<CurriculumClass>
    {
        Task<IEnumerable<CurriculumClass>> GetByAcademicYear(int academicYearId);

        Task<IEnumerable<CurriculumClass>> GetBySubject(int subjectId, int academicYearId);
    }
}
