using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Interfaces;
using MyPortal.Models;

namespace MyPortal.Services
{
    public abstract class IdentityService : MyPortalService
    {
        protected readonly IdentityContext Identity;
        protected readonly UserManager<ApplicationUser, string> UserManager;
        protected readonly UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
            IdentityUserClaim> UserStore;

        protected readonly RoleManager<ApplicationRole, string> RoleManager;
        protected readonly RoleStore<ApplicationRole> RoleStore;

        public IdentityService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Identity = new IdentityContext();
            UserStore = new UserStore<ApplicationUser, ApplicationRole, string, IdentityUserLogin, IdentityUserRole,
                IdentityUserClaim>(Identity);
            UserManager = new UserManager<ApplicationUser, string>(UserStore);
            RoleStore = new RoleStore<ApplicationRole>(Identity);
            RoleManager = new RoleManager<ApplicationRole, string>(RoleStore);
        }

        public IdentityService() : base()
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