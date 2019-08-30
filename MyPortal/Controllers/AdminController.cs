using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos.Identity;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Processes;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    
    [Authorize(Roles = "Admin")]
    [RoutePrefix("Staff")]
    public class AdminController : MyPortalIdentityController
    {
        [Route("Admin/Users/New")]
        public ActionResult NewUser()
        {
            return View();
        }

        [Route("Admin/Users/{id}")]
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

        [Route("Admin/Users")]
        public ActionResult Users()
        {
            return View();
        }

        [Route("Admin/Roles")]
        public ActionResult Roles()
        {
            var viewModel = new RolesViewModel();
            return View(viewModel);
        }

        [Route("Admin/Roles/Permission/{roleId}")]
        public async Task<ActionResult> RolePermissions(string roleId)
        {
            var viewModel = new RolePermissionsViewModel();

            var role = PrepareResponseObject(await AdminProcesses.GetRoleById_Model(roleId, _roleManager, _identity));

            viewModel.Role = role;

            return View(viewModel);
        }
    }
}