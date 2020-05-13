using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Exceptions;
using ClaimTypes = MyPortal.Logic.Constants.ClaimTypes;

namespace MyPortal.Logic.Authorisation
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        private readonly IApplicationUserService _userService;
        private readonly IApplicationRolePermissionService _rolePermissionService;
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> options, IApplicationUserService userService, IApplicationRolePermissionService rolePermissionService) : base(userManager, roleManager, options)
        {
            _userService = userService;
            _rolePermissionService = rolePermissionService;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            var appClaims = await GenerateClaimsForUserAsync(user);
            
            ((ClaimsIdentity) principal.Identity).AddClaims(appClaims);

            return principal;
        }

        private async Task<IEnumerable<Claim>> GenerateClaimsForUserAsync(ApplicationUser user)
        {
            var claims = new List<Claim>();

            var displayName = await _userService.GetDisplayName(user.Id);

            claims.Add(new Claim(ClaimTypes.UserType, user.UserType));
            claims.Add(new Claim(ClaimTypes.DisplayName, displayName));

            var roleNames = await UserManager.GetRolesAsync(user);

            foreach (var roleName in roleNames)
            {
                var roleId = (await RoleManager.FindByNameAsync(roleName)).Id;

                var perms = await _rolePermissionService.GetPermissionClaimValues(roleId);

                foreach (var perm in perms)
                {
                    claims.Add(new Claim(ClaimTypes.Permissions, perm));
                }
            }

            return claims;
        }
    }
}