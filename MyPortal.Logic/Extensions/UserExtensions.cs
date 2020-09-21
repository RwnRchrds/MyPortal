using System;
using System.Linq;
using System.Security.Claims;
using Google.Apis.Drive.v3.Data;
using MyPortal.Database.Constants;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Extensions
{
    public static class UserExtensions
    {
        public static string GetDisplayName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ApplicationClaimTypes.DisplayName)?.Value ?? user.Identity.Name;
        }

        public static bool HasPermission(this ClaimsPrincipal principal, params Guid[] permissions)
        {
            if (permissions.Length == 0)
            {
                return true;
            }

            foreach (var permission in permissions)
            {
                if (principal.HasClaim(ApplicationClaimTypes.Permission, permission.ToString("N")))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsType(this ClaimsPrincipal principal, int userType)
        {
            var hasType = int.TryParse(principal.FindFirst(ApplicationClaimTypes.UserType).Value, out var claimValue);

            return hasType && claimValue == userType;
        }
    }
}