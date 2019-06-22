using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.Controllers.Api
{
    public class MyPortalIdentityApiController : ApiController
    {
        protected readonly MyPortalDbContext _context;
        protected readonly ApplicationDbContext _identity;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly UserStore<ApplicationUser> _userStore;

        public MyPortalIdentityApiController()
        {
            _identity = new ApplicationDbContext();
            _userStore = new UserStore<ApplicationUser>(_identity);
            _userManager = new UserManager<ApplicationUser>(_userStore);
            _context = new MyPortalDbContext();
        }

        public MyPortalIdentityApiController(MyPortalDbContext context)
        {
            _identity = new ApplicationDbContext();
            _userStore = new UserStore<ApplicationUser>(_identity);
            _userManager = new UserManager<ApplicationUser>(_userStore);
            _context = context;
        }
    }
}
