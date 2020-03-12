using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Authorisation
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        private readonly IPersonService _personService;
        private readonly IApplicationRolePermissionService _rolePermissionService;
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> options, IPersonService personService, IApplicationRolePermissionService rolePermissionService) : base(userManager, roleManager, options)
        {
            _personService = personService;
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

            var person = await _personService.GetByUserId(user.Id);

            var displayName = user.UserName;

            if (user.UserType == UserTypeDictionary.Staff && person != null)
            {
                displayName = $"{person.Title} {person.FirstName.Substring(0, 1)} {person.LastName}";
            }
            else if (person != null)
            {
                displayName = $"{person.FirstName} {person.LastName}";
            }
            
            claims.Add(new Claim(ClaimTypeDictionary.UserType, user.UserType));
            claims.Add(new Claim(ClaimTypeDictionary.DisplayName, displayName));

            var roleNames = await UserManager.GetRolesAsync(user);

            foreach (var roleName in roleNames)
            {
                var roleId = (await RoleManager.FindByNameAsync(roleName)).Id;

                var perms = await _rolePermissionService.GetPermissionClaimValues(roleId);

                foreach (var perm in perms)
                {
                    claims.Add(new Claim(ClaimTypeDictionary.Permissions, perm));
                }
            }

            return claims;
        }
    }
}