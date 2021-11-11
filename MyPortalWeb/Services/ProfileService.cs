using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortalWeb.Services
{
    public class ProfileService : IProfileService
    {
        protected UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserService _userService;

        public ProfileService(UserManager<User> userManager, RoleManager<Role> roleManager, IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        public async System.Threading.Tasks.Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            var roles = await _userService.GetUserRoles(user.Id);

            var claims = new List<Claim>
            {
                new Claim(ApplicationClaimTypes.UserType, user.UserType.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Id.Value.ToString("N")));
            }

            context.IssuedClaims.AddRange(claims);
        }

        public async System.Threading.Tasks.Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
        
            context.IsActive = (user != null) && user.Enabled;
        }
    }
}