using System;
using System.Linq;
using System.Security.Principal;
using MyPortal.Models.Database;

namespace MyPortal.Processes
{
    public static class SystemProcesses
    {
        static SystemProcesses()
        {

        }

        public static int GetCurrentAcademicYearId(MyPortalDbContext context)
        {
            var academicYear =
                context.CurriculumAcademicYears.SingleOrDefault(x =>
                    x.FirstDate <= DateTime.Now && x.LastDate >= DateTime.Now);

            if (academicYear == null)
            {
                throw new Exception("Academic year not found.");
            }

            return academicYear.Id;
        }

        public static int GetCurrentOrSelectedAcademicYearId(MyPortalDbContext context, IPrincipal user)
        {
            var academicYearId = GetCurrentAcademicYearId(context);
            
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