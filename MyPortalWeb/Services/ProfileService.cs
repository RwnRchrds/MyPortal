using System.Collections.Generic;
using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Entity;

namespace MyPortalWeb.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;

        public ProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>
            {
                new(ApplicationClaimTypes.UserType, user.UserType.ToString())
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async System.Threading.Tasks.Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null);
        }
    }
}