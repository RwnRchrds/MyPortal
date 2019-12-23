using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.BusinessLogic.Services;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Models.Identity;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
    [RoutePrefix("Home")]
    public class HomeController : MyPortalController
    {
        public async Task<ActionResult> Index()
        {
            using (var staffService = new StaffMemberService())
            using (var curriculumService = new CurriculumService())
            {
                var userId = User.Identity.GetUserId();

                var staff = await staffService.TryGetStaffMemberByUserId(userId);

                var academicYears = await curriculumService.GetAcademicYears();

                var selectedAcademicYearId = await User.GetSelectedOrCurrentAcademicYearId();

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
}