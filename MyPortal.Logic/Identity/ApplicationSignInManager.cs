using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Identity
{
    public class ApplicationSignInManager : SignInManager<User>
    {
        public ApplicationSignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor,
            logger, schemes, confirmation)
        {
        }

        public override async Task<bool> CanSignInAsync(User user)
        {
            if (!user.Enabled)
            {
                Logger.LogWarning(4, "User {userId} cannot sign in as the account is currently disabled.",
                    await UserManager.GetUserIdAsync(user));
                return false;
            }
            
            return await base.CanSignInAsync(user);
        }
    }
}