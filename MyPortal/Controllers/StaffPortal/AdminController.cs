using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyPortal.Attributes.MvcAuthorise;
using MyPortal.Models;
using MyPortal.Services;
using MyPortal.ViewModels;

namespace MyPortal.Controllers.StaffPortal
{
    [UserType(UserType.Staff)]
    [RoutePrefix("Staff/Admin")]
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
        public ActionResult UserDetails(string id)
        {
            var user = _identity.Users
                .SingleOrDefault(x => x.Id == id);

            if (user == null)
                return HttpNotFound();

            var userRoles = _userManager.GetRolesAsync(id).Result;

            var roles = _identity.Roles.OrderBy(x => x.Name).ToList();

            var attachedProfile = "";

            var studentProfile = _context.Students.SingleOrDefault(x => x.Person.UserId == user.Id);
            var staffProfile = _context.StaffMembers.SingleOrDefault(x => x.Person.UserId == user.Id);

            if (studentProfile != null)
                attachedProfile = studentProfile.Person.LastName + ", " + studentProfile.Person.FirstName + " (Student)";

            else if (staffProfile != null)
                attachedProfile = staffProfile.Person.LastName + ", " + staffProfile.Person.FirstName + " (Staff)";

            var viewModel = new UserDetailsViewModel
            {
                User = user,
                UserRoles = userRoles,
                Roles = roles,
                AttachedProfileName = attachedProfile
            };

            return View(viewModel);
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
            var viewModel = new RolePermissionsViewModel();

            var result = await AdminService.GetRoleById(roleId, _roleManager);

            viewModel.Role = result;

            return View(viewModel);
        }
    }
}