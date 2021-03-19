using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic
{
    public class IdentityServiceCollection : IIdentityServiceCollection
    {
        public IdentityServiceCollection(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IRolePermissionsCache rolePermissionsCache)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            RolePermissionsCache = rolePermissionsCache;
        }

        public UserManager<User> UserManager { get; }

        public RoleManager<Role> RoleManager { get; }

        public SignInManager<User> SignInManager { get; }

        public IRolePermissionsCache RolePermissionsCache { get; }

        public void Dispose()
        {
            UserManager.Dispose();
            RoleManager.Dispose();
        }
    }
}
