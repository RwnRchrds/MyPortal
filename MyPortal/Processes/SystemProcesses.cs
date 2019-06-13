using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web.Helpers;
using Microsoft.Ajax.Utilities;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using Syncfusion.EJ2.Base;

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

        public static ApiResponse<T> PerformDataOperations<T>(this IEnumerable<T> dataSource, DataManagerRequest dm)
        {
            DataOperations operation = new DataOperations();

            if (dm.Search != null && dm.Search.Count > 0)
            {
                dataSource = operation.PerformSearching(dataSource, dm.Search);
            }

            if (dm.Sorted != null && dm.Sorted.Count > 0)
            {
                dataSource = operation.PerformSorting(dataSource, dm.Sorted);
            }

            if (dm.Where != null && dm.Where.Count > 0)
            {
                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
            }

            var count = dataSource.Count();

            if (dm.Skip != 0)
            {
                dataSource = operation.PerformSkip(dataSource, dm.Skip);
            }

            if (dm.Take != 0)
            {
                dataSource = operation.PerformTake(dataSource, dm.Take);
            }

            return new ApiResponse<T> {Count = count, Items = dataSource};
        }
    }
}