using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using MyPortal.Interfaces;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Services;

namespace MyPortal.Repositories
{
    public class CurriculumAcademicYearRepository : Repository<CurriculumAcademicYear>, ICurriculumAcademicYearRepository
    {
        public CurriculumAcademicYearRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<CurriculumAcademicYear> GetCurrent()
        {
            return await Context.CurriculumAcademicYears.SingleOrDefaultAsync(x =>
                       x.FirstDate >= DateTime.Today && x.LastDate <= DateTime.Today) ?? await Context
                       .CurriculumAcademicYears.OrderByDescending(x => x.FirstDate).FirstOrDefaultAsync();
        }

        public async Task<CurriculumAcademicYear> GetCurrentOrSelected(IPrincipal user)
        {
            var academicYear = await GetCurrent();

            if (await user.HasPermissionAsync("ChangeAcademicYear"))
            {
                var userId = user.Identity.GetUserId();
                var selectedAcademicYearId = await Context.Database
                    .SqlQuery<int?>("SELECT SelectedAcademicYearId FROM AspNetUsers WHERE Id = @id",
                        new SqlParameter("@id", userId)).SingleOrDefaultAsync();

                if (selectedAcademicYearId != null)
                {
                    academicYear = await Context.CurriculumAcademicYears.FindAsync(selectedAcademicYearId);
                }
            }

            return academicYear;
        }
    }
}