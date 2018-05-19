using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Models;
using MyPortal.ViewModels;

namespace MyPortal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private MyPortalDbContext _context;        
        private ApplicationDbContext _identity;
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
            _context.Dispose();
            _identity.Dispose();
        }

        // GET: Admin
        [Route("Staff/Admin")]
        public ActionResult Index()
        {
            return View();
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

            var roles = _identity.Roles.ToList();

            var viewModel = new UserDetailsViewModel
            {
                User = user,
                UserRoles = userRoles,
                Roles = roles
            };

            return View(viewModel);
        }
    }
}