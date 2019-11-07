using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
    [RoutePrefix("Staff")]
    public class StaffController : MyPortalController
    {
        [Route("Staff")]
        [RequiresPermission("ViewStaff")]
        public ActionResult Staff()
        {
            var viewModel = new NewStaffViewModel();
            return View(viewModel);
        }

        [RequiresPermission("ViewStaff")]
        [Route("Staff/{id:int}", Name = "PeopleStaffDetails")]
        public async Task<ActionResult> StaffDetails(int id)
        {
            using (var personnelService = new PersonnelService(UnitOfWork))
            using (var staffService = new StaffMemberService(UnitOfWork))
            {
                var staff = await staffService.GetStaffMemberById(id);

                var userId = User.Identity.GetUserId();

                var currentStaffId = 0;

                var currentUser = await staffService.GetStaffMemberByUserId(userId);

                if (currentUser != null)
                {
                    currentStaffId = currentUser.Id;
                }

                var certificates = await personnelService.GetCertificatesByStaffMember(id);

                var courses = await personnelService.GetAllTrainingCourses();

                var viewModel = new StaffDetailsViewModel
                {
                    Staff = staff,
                    TrainingCertificates = certificates,
                    TrainingCourses = courses,
                    CurrentStaffId = currentStaffId
                };

                return View(viewModel);
            }
        }
    }
}