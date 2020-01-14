using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.BusinessLogic.Services;
using MyPortal.Models.Identity;

namespace MyPortal.Services
{
    public abstract class IdentityService
    {
        protected readonly IdentityContext Identity;
        protected readonly UserManager<ApplicationUser, string> UserManager;
        protected readonly UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
            IdentityUserClaim> UserStore;

        protected readonly RoleManager<ApplicationRole, string> RoleManager;
        protected readonly RoleStore<ApplicationRole> RoleStore;

        public IdentityService()
        {
            Identity = new IdentityContext();
            UserStore = new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
                IdentityUserClaim>(Identity);
            UserManager = new UserManager<ApplicationUser, string>(UserStore);
            RoleStore = new RoleStore<ApplicationRole>(Identity);
            RoleManager = new RoleManager<ApplicationRole, string>(RoleStore);
        }
    }
}