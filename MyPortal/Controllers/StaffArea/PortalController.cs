using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Services;
using MyPortal.ViewModels;

namespace MyPortal.Controllers.StaffPortal
{
    [UserType(UserType.Staff)]
    [RoutePrefix("Staff")]
    public class PortalController : MyPortalController
    {
        [Route("Home")]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();

            var staff = await StaffMemberService.GetStaffMemberFromUserId(userId, _context);

            var academicYears = await CurriculumService.GetAcademicYearsModel(_context);

            var selectedAcademicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(_context, User);

            var viewModel = new StaffHomeViewModel
            {
                CurrentUser = staff,
                CurriculumAcademicYears = academicYears,
                SelectedAcademicYearId = selectedAcademicYearId
            };

            return View(viewModel);
        }
    }
}