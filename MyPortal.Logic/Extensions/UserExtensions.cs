using System.Linq;
using System.Security.Claims;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Helpers;
using ClaimTypes = MyPortal.Logic.Constants.ClaimTypes;

namespace MyPortal.Logic.Extensions
{
    public static class UserExtensions
    {
        public static string GetDisplayName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.DisplayName)?.Value ?? user.Identity.Name;
        }

        public static bool HasPermission(this ClaimsPrincipal principal, params int[] permissions)
        {
            return permissions.Any(x => principal.HasClaim(ClaimTypes.Permissions, x.ToString()));
        }
    }
}