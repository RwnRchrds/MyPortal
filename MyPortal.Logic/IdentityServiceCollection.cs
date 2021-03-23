using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic
{
    public class IdentityServiceCollection : IIdentityServiceCollection
    {
        public IdentityServiceCollection(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
        }

        public UserManager<User> UserManager { get; }

        public RoleManager<Role> RoleManager { get; }

        public SignInManager<User> SignInManager { get; }

        public void Dispose()
        {
            UserManager.Dispose();
            RoleManager.Dispose();
        }
    }
}
