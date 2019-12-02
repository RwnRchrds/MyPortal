using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface ICurriculumAcademicYearRepository : IReadWriteRepository<CurriculumAcademicYear>
    {
        Task<CurriculumAcademicYear> GetCurrent();

        Task<CurriculumAcademicYear> GetCurrentOrSelected(IPrincipal user);
    }
}
