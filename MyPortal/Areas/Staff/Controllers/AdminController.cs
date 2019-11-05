using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Areas.Staff.ViewModels;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Controllers;
using MyPortal.Extensions;
using MyPortal.Models;
using MyPortal.Services;

namespace MyPortal.Areas.Staff.Controllers
{
    [UserType(UserType.Staff)]
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
        [Route("Users/{id}", Name = "AdminUserDetails")]
        public async Task<ActionResult> UserDetails(string id)
        {
            using (var adminService = new AdminService(UnitOfWork))
            using (var staffService = new StaffMemberService(UnitOfWork))
            using (var studentService = new StudentService(UnitOfWork))
            {
                var user = await adminService.GetUserById(id);

                if (user == null)
                    return HttpNotFound();

                var userRoles = await adminService.GetRolesByUser(id);

                var roles = await adminService.GetAllRoles();

                var attachedProfile = "";

                var studentProfile = await studentService.GetStudentFromUserId(id);
                var staffProfile = await staffService.GetStaffMemberFromUserId(id);

                if (studentProfile != null)
                    attachedProfile = $"{studentProfile.GetDisplayName()} (Student)";

                else if (staffProfile != null)
                    attachedProfile = $"{staffProfile.GetDisplayName()} (Staff)";

                var viewModel = new UserDetailsViewModel
                {
                    User = user,
                    UserRoles = userRoles,
                    Roles = roles,
                    AttachedProfileName = attachedProfile
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
        [Route("Roles/Permissions/{roleId}", Name = "AdminRolePermissions")]
        public async Task<ActionResult> RolePermissions(string roleId)
        {
            using (var adminService = new AdminService(UnitOfWork))
            {
                var viewModel = new RolePermissionsViewModel();

                var result = await adminService.GetRoleById(roleId);

                viewModel.Role = result;

                return View(viewModel);
            }
        }
    }
}