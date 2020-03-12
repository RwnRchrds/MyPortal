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
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> options, IPersonService personService) : base(userManager, roleManager, options)
        {
            _personService = personService;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            

            ((ClaimsIdentity) principal.Identity).AddClaim(new Claim(ClaimTypeDictionary.DisplayName, displayName));

            return principal;
        }

        private IEnumerable<Claim> GenerateClaimsForUser(ApplicationUser user)
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

            return claims;
        }
    }
}