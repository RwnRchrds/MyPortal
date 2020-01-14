using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.BusinessLogic.Models.Identity;

namespace MyPortal.Controllers.Api
{
    public abstract class MyPortalIdentityApiController : MyPortalApiController
    {
        protected readonly IdentityContext Identity;
        protected readonly UserManager<ApplicationUser, string> UserManager;
        protected readonly UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
            IdentityUserClaim> UserStore;

        protected readonly RoleManager<ApplicationRole, string> RoleManager;
        protected readonly RoleStore<ApplicationRole> RoleStore;

        public MyPortalIdentityApiController()
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
