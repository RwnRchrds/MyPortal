using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Authorisation
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        private readonly IAcademicYearService _service;
        public ApplicationSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation, IAcademicYearService service) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _service = service;
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var userInDb = await UserManager.FindByNameAsync(userName);

            await UserManager.UpdateAsync(userInDb);

            if (userInDb == null)
            {
                return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
            }

            if (userInDb.Enabled)
            {
                var result = await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

                if (result.Succeeded)
                {
                    await SetAcademicYear(userInDb);
                }

                return result;
            }
            
            var signInResult = await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

            return signInResult.Succeeded ? SignInResult.LockedOut : signInResult;
        }

        private async Task SetAcademicYear(ApplicationUser user)
        {
            var currentYear = await _service.GetCurrent();

            user.SelectedAcademicYearId = currentYear?.Id;
        }
    }
}
