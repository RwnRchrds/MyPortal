using System;
using System.Linq;
using System.Security.Principal;
using MyPortal.Models.Database;

namespace MyPortal.Processes
{
    public static class ContextProcesses
    {
        public static int GetAcademicYearId(IPrincipal user, MyPortalDbContext context)
        {
            int academicYearId;

            if (context.IsDebug)
            {
                var academicYear = context.CurriculumAcademicYears.SingleOrDefault(x => x.Name == "First");
                if (academicYear == null)
                {
                    throw new Exception("Academic Year Not Found.");
                }

                academicYearId = academicYear.Id;
            }
            else
            {
                academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(user);
            }

            return academicYearId;
        }
    }
}