using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Models;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    //MyPortal Admin Portal Controller --> Controller methods for Admin Portal
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly MyPortalDbContext _context;
        private readonly ApplicationDbContext _identity;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController()
        {
            _context = new MyPortalDbContext();
            _identity = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(_identity);
            _userManager = new UserManager<ApplicationUser>(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
                _identity.Dispose();
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }

        // Admin | Users --> Users List (All)
        [Route("Staff/Admin/Users")]
        public ActionResult Users()
        {
            return View();
        }

        // Admin | Users | X --> User Details (for User X)
        [Route("Staff/Admin/Users/{id}")]
        public ActionResult UserDetails(string id)
        {
            var user = _identity.Users
                .SingleOrDefault(x => x.Id == id);

            if (user == null)
                return HttpNotFound();

            var userRoles = _userManager.GetRolesAsync(id).Result;

            var roles = _identity.Roles.OrderBy(x => x.Name).ToList();

            var attachedProfile = "";

            var studentProfile = _context.Students.SingleOrDefault(x => x.UserId == user.Id);
            var staffProfile = _context.Staff.SingleOrDefault(x => x.UserId == user.Id);

            if (studentProfile != null)
                attachedProfile = studentProfile.LastName + ", " + studentProfile.FirstName + " (Student)";

            else if (staffProfile != null)
                attachedProfile = staffProfile.LastName + ", " + staffProfile.FirstName + " (Staff)";

            var viewModel = new UserDetailsViewModel
            {
                User = user,
                UserRoles = userRoles,
                Roles = roles,
                AttachedProfileName = attachedProfile
            };

            return View(viewModel);
        }

        // Admin | Users | New User --> New User Form
        [Route("Staff/Admin/Users/New")]
        public ActionResult NewUser()
        {
            return View();
        }
    }
}