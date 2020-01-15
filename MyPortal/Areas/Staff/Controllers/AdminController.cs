using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Areas.Staff.ViewModels.Admin;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Interfaces;
using MyPortal.BusinessLogic.Models.Identity;
using MyPortal.BusinessLogic.Services;
using MyPortal.BusinessLogic.Services.Identity;
using MyPortal.Controllers;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
    [RouteArea("Staff")]
    [RoutePrefix("Admin")]
    public class AdminController : MyPortalIdentityController
    {
        [RequiresPermission("EditUsers")]
        [Route("Users/New", Name = "AdminNewUser")]
        public ActionResult NewUser()
        {
            return View();
        }

        [RequiresPermission("EditUsers")]
        [Route("Users/{userId}")]
        public async Task<ActionResult> UserDetails(string userId)
        {
            using (var adminService = new AdminService())
            using (var staffService = new StaffMemberService())
            using (var studentService = new StudentService())
            {
                var user = await adminService.GetUserById(userId);

                if (user == null)
                    return HttpNotFound();

                var userRoles = await adminService.GetRolesByUser(userId);

                var roles = await adminService.GetAllRoles();

                StudentDto studentProfile = await studentService.TryGetStudentByUserId(userId);

                StaffMemberDto staffProfile = await staffService.TryGetStaffMemberByUserId(userId);

                IPersonDto attachedProfile = null;

                if (studentProfile != null)
                    attachedProfile = studentProfile;

                else if (staffProfile != null)
                    attachedProfile = staffProfile;

                var viewModel = new UserDetailsViewModel
                {
                    User = user,
                    UserRoles = userRoles,
                    Roles = roles,
                    AttachedProfile = attachedProfile
                };

                return View(viewModel);
            }
        }

        [RequiresPermission("EditUsers")]
        [Route("Users", Name = "AdminUsers")]
        public ActionResult Users()
        {
            return View();
        }

        [RequiresPermission("EditRoles")]
        [Route("Roles", Name = "AdminRoles")]
        public ActionResult Roles()
        {
            var viewModel = new RolesViewModel();
            return View(viewModel);
        }

        [RequiresPermission("EditRoles")]
        public async Task<ActionResult> RolePermissions(string roleId)
        {
            using (var adminService = new AdminService())
            {
                var viewModel = new RolePermissionsViewModel();

                var result = await adminService.GetRoleById(roleId);

                viewModel.Role = result;

                return View(viewModel);
            }
        }
    }
}