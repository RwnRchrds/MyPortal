using System;
using System.Linq;
using System.Security.Principal;
using MyPortal.Models.Database;

namespace MyPortal.Processes
{
    public static class SystemProcesses
    {
        private static readonly MyPortalDbContext _context;

        static SystemProcesses()
        {
            _context = new MyPortalDbContext();
        }

        public static int GetCurrentAcademicYearId()
        {
            var academicYear =
                _context.CurriculumAcademicYears.SingleOrDefault(x =>
                    x.FirstDate <= DateTime.Now && x.LastDate >= DateTime.Now);

            if (academicYear == null)
            {
                throw new Exception("Academic year not found.");
            }

            return academicYear.Id;
        }

        public static int GetCurrentOrSelectedAcademicYearId(IPrincipal user)
        {
            var academicYearId = GetCurrentAcademicYearId();
            
            if (user != null && user.IsInRole("Staff"))
            {
                var selectedAcademicYearId = user.GetSelectedAcademicYearId();

                if (selectedAcademicYearId != null)
                {
                    academicYearId = (int) selectedAcademicYearId;
                }
            }

            return academicYearId;
        }
    }
}