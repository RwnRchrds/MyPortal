using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.Helpers
{
    public static class ContextHelper
    {
        public static int GetAcademicYearId(IPrincipal User, MyPortalDbContext context)
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
                academicYearId = SystemHelper.GetCurrentOrSelectedAcademicYearId(User);
            }

            return academicYearId;
        }
    }
}