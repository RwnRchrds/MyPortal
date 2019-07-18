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
    public class MyPortalIdentityApiController : MyPortalApiController
    {
        protected readonly IdentityContext _identity;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly UserStore<ApplicationUser> _userStore;

        public MyPortalIdentityApiController()
        {
            _identity = new IdentityContext();
            _userStore = new UserStore<ApplicationUser>(_identity);
            _userManager = new UserManager<ApplicationUser>(_userStore);
        }

        public MyPortalIdentityApiController(MyPortalDbContext context, IdentityContext identity) : base(context)
        {
            _identity = new IdentityContext();
            _userStore = new UserStore<ApplicationUser>(_identity);
            _userManager = new UserManager<ApplicationUser>(_userStore);
        }
    }
}
