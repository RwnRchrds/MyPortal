using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface ICurriculumAcademicYearRepository : IRepository<CurriculumAcademicYear>
    {
        Task<CurriculumAcademicYear> GetCurrentAcademicYear();

        Task<CurriculumAcademicYear> GetCurrentOrSelectedAcademicYear(IPrincipal user);
    }
}
