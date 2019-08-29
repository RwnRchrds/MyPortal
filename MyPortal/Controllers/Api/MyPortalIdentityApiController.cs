using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.Controllers.Api
{
    public class MyPortalIdentityApiController : MyPortalApiController
    {
        protected readonly IdentityContext _identity;
        protected readonly UserManager<ApplicationUser, string> _userManager;
        protected readonly UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
            IdentityUserClaim> _userStore;

        protected readonly RoleManager<ApplicationRole, string> _roleManager;
        protected readonly RoleStore<ApplicationRole> _roleStore;

        public MyPortalIdentityApiController()
        {
            _identity = new IdentityContext();
            _userStore = new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
                IdentityUserClaim>(_identity);
            _userManager = new UserManager<ApplicationUser, string>(_userStore);
            _roleStore = new RoleStore<ApplicationRole>(_identity);
            _roleManager = new RoleManager<ApplicationRole, string>(_roleStore);
        }

        public MyPortalIdentityApiController(MyPortalDbContext context, IdentityContext identity) : base(context)
        {
            _identity = new IdentityContext();
            _userStore = new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
                IdentityUserClaim>(_identity);
            _userManager = new UserManager<ApplicationUser, string>(_userStore);
            _roleStore = new RoleStore<ApplicationRole>(_identity);
            _roleManager = new RoleManager<ApplicationRole, string>(_roleStore);
        }
    }
}
